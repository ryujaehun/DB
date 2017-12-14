import java.awt.Dimension;
import java.awt.Event;
import java.awt.GraphicsConfiguration;
import java.awt.GridLayout;
import java.awt.HeadlessException;
import java.awt.Point;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.Window;
import java.awt.datatransfer.UnsupportedFlavorException;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.AdjustmentListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.IOException;
import java.lang.reflect.Method;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.StringTokenizer;

import javax.swing.Action;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.JViewport;
import javax.swing.KeyStroke;
import javax.swing.ScrollPaneConstants;
import javax.swing.SwingUtilities;
import javax.swing.TransferHandler;
import javax.swing.border.EmptyBorder;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

/**
  * EReditor
  * ERFrame.java
  * Created by Palle Klewitz on 01.11.2015
  * Copyright (c) 2014 - 2017 Palle.
  * Permission is hereby granted, free of charge, to any person obtaining a copy
  * of this software and associated documentation files (the "Software"), to deal
  * in the Software without restriction, including without limitation the rights
  * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  * copies of the Software, and to permit persons to whom the Software is
  * furnished to do so, subject to the following conditions:
  *
  * The above copyright notice and this permission notice shall be included in
  * all copies or substantial portions of the Software.
  *
  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
  * THE SOFTWARE.
  */

public class ERFrame extends JFrame implements ActionListener, ERHistoryChangeNotifier, ItemListener
{
	private static final long	serialVersionUID	= -3678869730031898536L;
	private JScrollPane			spERView;
	private ERView				erView;
	private EntityEditor		editor;
	private JTextField 			schemaField;
	
	private JMenuItem	menuNew, menuOpen, menuClose, menuSave, menuSaveAs, menuExportImage, menuExportModel, menuSaveToDB, menuLoadFromDB, menuLoadQuiz1;	//데이터 베이스에 저장, 불러오기
	private JMenuItem	menuCut, menuCopy, menuPaste, menuDelete, menuUndo, menuRedo, menuSelectAll;
	private JMenuItem	menuAddEntity, menuAddRelationship, menuAddDescriptionBox;
	private JMenuItem	menuZoomOriginal, menuZoomIn, menuZoomOut, menuExpand, menuImplode;
	private JMenuItem	menuItemHelp, menuItemAbout;
	
	JFrame checkFrame;	//체크 창 프레임
	JLabel label;		//라벨 
	JButton chkBtn;//체크 버튼
	
	private JMenu		menuExport;
	
	private ERChangeHistory changeHistory;
	private JFrame		setSchemaString;
	
	private JFrame		chooseSchema;			//스키마 선택하는 프레임
	private JScrollPane schemaScroll;
	private JList 		schemaList;
	ResultSet rs;
	public ERFrame() throws HeadlessException
	{
		super();
		init();
	}
	
	public ERFrame(GraphicsConfiguration gc)
	{
		super(gc);
		init();
	}
	
	public ERFrame(String title) throws HeadlessException
	{
		super(title);
		init();
	}
	
	public ERFrame(String title, GraphicsConfiguration gc)
	{
		super(title, gc);
		init();
	}
	
	@Override
	public void actionPerformed(final ActionEvent e)
	{
		final JFrame self = this;
		SwingUtilities.invokeLater(() ->
		{
			if (e.getSource()==menuSaveToDB) {
				//DB에 저장할 시 메뉴 구현
				setSchemaString = new JFrame();
				setSchemaString.setLayout(new GridLayout(2, 1));
				setSchemaString.add(new JLabel("저장할 스카마 명을 입력하세요"));
				setSchemaString.setSize(300, 150);
				setSchemaString.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
				setSchemaString.setVisible(true);
				schemaField = new JTextField();
				setSchemaString.add(schemaField);
				schemaField.addActionListener(this);
				
			}
			else if(e.getSource()==schemaField) {
				String schemaName = schemaField.getText();
				System.out.println(schemaName);
				try {
					erView.model.SaveToDB(schemaName);
					setSchemaString.dispose();
				} catch (SQLException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
			else if(e.getSource()==menuLoadQuiz1) {
				erView.model.ModelClear();
				erView.repaint();
				
				DescriptionBox db= new DescriptionBox();
				db.setBounds(new Rectangle(100, 100, 50, 50));
				db.setName("Quiz1");
				db.setText("① 직원은  직원번호, 이름, 직위, 전화번호를 갖는다.\r\n" + 
						"② 프로젝트는 과제번호, 예산을 갖는다.\r\n" + 
						"③ 한 직원은 여러 개의 프로젝트 작업을 한다.");
				erView.model.descriptions.add(db);
				erView.repaint();
				
				checkFrame =new JFrame();
				label =new JLabel();
				label.setText("정답률: ");
				chkBtn =new JButton("정답확인!");
				chkBtn.addActionListener(this);
				checkFrame.setLayout(new GridLayout(2, 1));
				checkFrame.add(label);
				checkFrame.add(chkBtn);
				
				checkFrame.setSize(300, 300);
				checkFrame.setVisible(true);
				
				
			}
			else if(e.getSource()==menuLoadFromDB) {
				//DB에서 불러올 시 메뉴 구현
				chooseSchema = new JFrame();
				chooseSchema.setLayout(new GridLayout(1,1));
				chooseSchema.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

				ConnTibero  ct = new ConnTibero();
				ct.connect();
				ArrayList<String> schemaArrayList =new ArrayList<String>();
				rs = ct.excute("SELECT schema_id, name FROM schema;");
				try {
					while(rs.next()) {
						String schema = rs.getInt(1) +","+ rs.getString(2);
						schemaArrayList.add(schema);
					}
					String[] schemaArray = new String[schemaArrayList.size()];
					for(int i=0; i<schemaArrayList.size(); i++)
					{
						schemaArray[i] = schemaArrayList.get(i);
					}
					
					schemaList = new JList(schemaArray);
					schemaScroll = new JScrollPane(schemaList);
					chooseSchema.add(schemaScroll);
					
					ListSelectionListener lisetSelectionLisetner = new ListSelectionListener() {
						
						@Override
						public void valueChanged(ListSelectionEvent e) {
							// TODO Auto-generated method stub
							boolean adjust = e.getValueIsAdjusting();
							if(!adjust) {
								JList list = (JList)e.getSource();
								int selections[] = list.getSelectedIndices();
								int openSchemaId;	//오픈할 스키마 아이디
								Object selectionValues[] = list.getSelectedValues();
								for (int i=0, n =selections.length; i<n; i++) {
									StringTokenizer st = new StringTokenizer(selectionValues[i].toString(), ",");
									openSchemaId =Integer.parseInt(st.nextToken());
									try {
										erView.model.LoadFromDB(openSchemaId);
										erView.repaint();
										chooseSchema.dispose();
										self.setFocusable(true);
									} catch (SQLException e1) {
										// TODO Auto-generated catch block
										e1.printStackTrace();
									}
								}
							}
						}
					};
					schemaList.addListSelectionListener(lisetSelectionLisetner);
				} catch (SQLException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
				chooseSchema.setVisible(true);
				chooseSchema.setSize(400, 400);
			}
			else if(e.getSource()==chkBtn) {
				try {
					int rate;
					rate = erView.model.checkModelwithQuiz(29);
					label.setText("정답률: " + rate + "%" );
					checkFrame.revalidate();
				} catch (SQLException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
			}
			if (e.getSource() == menuNew)
			{
				// if (!erView.model.isEmpty())
				// if (JOptionPane.showConfirmDialog(self,
				// ER_Editor.LOCALIZATION.getString("save_confirmation"),
				// ER_Editor.LOCALIZATION.getString("save_confirm_option"),
				// JOptionPane.YES_NO_OPTION) == JOptionPane.OK_OPTION)
				// erView.model.save(self);
				// changeHistory.reset();
				ERFrame newFrame = new ERFrame();
				newFrame.setModel(new ERModel());
				// erView.model = new ERModel();
			}
			else if (e.getSource() == menuOpen)
			{
				// if (!erView.model.isEmpty())
				// if (JOptionPane.showConfirmDialog(self,
				// ER_Editor.LOCALIZATION.getString("save_confirmation"),
				// ER_Editor.LOCALIZATION.getString("save_confirm_option"),
				// JOptionPane.YES_NO_OPTION) == JOptionPane.OK_OPTION)
				// erView.model.save(self);
				// changeHistory.reset();
				Thread loadThread = new Thread(() ->
				{
					ERModel model = ERModel.open(self);
					SwingUtilities.invokeLater(() ->
					{
						ERFrame newFrame = new ERFrame();
						newFrame.setModel((model == null) ? new ERModel() : model);
					});
				});
				loadThread.start();
			}
			else if (e.getSource() == menuClose)
				self.dispatchEvent(new WindowEvent(self, WindowEvent.WINDOW_CLOSING));
			else if (e.getSource() == menuSave)
			{
				getRootPane().putClientProperty("Window.documentModified", Boolean.TRUE);
				erView.model.save(self);
			}
			else if (e.getSource() == menuSaveAs)
				erView.model.saveAs(self);
			else if (e.getSource() == menuExportImage)
				erView.model.export();
			else if (e.getSource() == menuExportModel)
				erView.model.exportModel();
			else if (e.getSource() == menuUndo)
			{
				if (changeHistory.canUndo())
				{
					changeHistory.undo();
					erView.deselectAll();
				}
			}
			else if (e.getSource() == menuRedo)
			{
				if (changeHistory.canRedo())
				{
					changeHistory.redo();
					erView.deselectAll();
				}
			}
			else if (e.getSource() == menuCut)
			{
				if (erView.isFocusOwner())
					try
					{
						erView.cutSelected();
					}
					catch (IOException e1)
					{
						e1.printStackTrace();
					}
				// TODO CUT FOR EntityEditor/RelationshipEditor
			}
			else if (e.getSource() == menuCopy)
			{
				if (erView.isFocusOwner())
					try
					{
						erView.copySelected();
					}
					catch (IOException e1)
					{
						e1.printStackTrace();
					}
				// TODO COPY FOR EntityEditor/RelationshipEditor
			}
			else if (e.getSource() == menuPaste)
			{
				if (erView.isFocusOwner())
					try
					{
						erView.paste();
					}
					catch (HeadlessException | ClassNotFoundException | UnsupportedFlavorException | IOException e1)
					{
						e1.printStackTrace();
					}
				// TODO PASTE FOR EntityEditor/RelationshipEditor
			}
			else if (e.getSource() == menuSelectAll)
				erView.selectAll();
			else if (e.getSource() == menuDelete)
				erView.deleteSelected();
			else if (e.getSource() == menuAddEntity)
				erView.addEntity();
			else if (e.getSource() == menuAddRelationship)
				erView.requestRelationship();
			else if (e.getSource() == menuAddDescriptionBox)
				erView.addDescriptionBox();
			else if (e.getSource() == menuZoomOriginal)
				erView.zoomOriginal();
			else if (e.getSource() == menuZoomIn)
				erView.zoomIn();
			else if (e.getSource() == menuZoomOut)
				erView.zoomOut();
			else if (e.getSource() == menuImplode)
				erView.shrink();
			else if (e.getSource() == menuExpand)
				erView.expand();
			else if (e.getSource() == menuItemHelp)
				new HelpWindow();
			else if (e.getSource() == menuItemAbout)
				showAboutWindow();
			erView.repaint();
			setTitle("Recursive_ER");
		});
	}
	
	public ERModel getModel()
	{
		return erView.model;
	}
	
	@Override
	public void historyDidChange(ERChangeHistory history)
	{
		menuUndo.setEnabled(history.canUndo());
		menuRedo.setEnabled(history.canRedo());
		if (history.canUndo())
		{
			menuUndo.setText(String.format(ER_Editor.LOCALIZATION.getString("undo_with_format"), history.peekUndo()));
			getRootPane().putClientProperty("Window.documentModified", Boolean.TRUE);
		}
		else
		{
			menuUndo.setText(ER_Editor.LOCALIZATION.getString("undo"));
			getRootPane().putClientProperty("Window.documentModified", Boolean.FALSE);
		}
		if (history.canRedo())
			menuRedo.setText(String.format(ER_Editor.LOCALIZATION.getString("redo_with_format"), history.peekRedo()));
		else
			menuRedo.setText(ER_Editor.LOCALIZATION.getString("redo"));
	}
	
	public void setModel(ERModel model)
	{
		erView.model = model;
		getRootPane().putClientProperty("Window.documentFile", erView.model.fileName);
		setTitle("Recursive_ER");
	}
	
	public void showAboutWindow()
	{
		JDialog about = new JDialog(this);
		about.setBounds(about.getParent().getX() + about.getParent().getWidth() / 2 - 100,
				about.getParent().getY() + about.getParent().getHeight() / 2 - 100, 200, 150);
		about.setLayout(null);
		about.setResizable(false);
		
		JLabel appName = new JLabel();
		appName.setBounds(0, 0, 200, 120);
		appName.setText("<html><head><style>body { text-align: center; width: 150px; }</style></head>"
				+ "<body><h1 style=\"font-weight:100;font-size:16px;\">ER-Editor</h1><p style=\"font-size:9px;\">v3.6.1<br><br>&copy; 2014 - 2017 Palle</p>");
		about.add(appName);
		about.setVisible(true);
	}
	//gui 설정
	@SuppressWarnings("MagicConstant")
	private void init()
	{
		final ERFrame self = this;
		setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		ER_FrameLayout layout = new ER_FrameLayout();
		setLayout(layout);
		setBounds(0, 0, 1280, 720);
		setLocationByPlatform(true);
		if (!ER_Editor.openedFrames.isEmpty())
		{
			setLocationRelativeTo(ER_Editor.openedFrames.get(ER_Editor.openedFrames.size() - 1));
			Point location = getLocation();
			location.translate(22, 22);
			setLocation(location);
		}
		setIconImage(Toolkit.getDefaultToolkit().getImage(getClass().getResource("icon.png")));
		addWindowListener(new WindowAdapter()
		{
			@Override
			public void windowClosing(WindowEvent e)
			{
				if (!erView.model.isEmpty() && erView.model.needsSave())
					if (JOptionPane.showConfirmDialog(self, ER_Editor.LOCALIZATION.getString("save_confirmation"), ER_Editor.LOCALIZATION.getString("save_confirm_option"),
							JOptionPane.YES_NO_OPTION) == JOptionPane.OK_OPTION)
						erView.model.save(self);
				self.removeWindowListener(this);
				self.setVisible(false);
				self.dispose();
				ER_Editor.openedFrames.remove(self);
			}
		});
		
		try
		{
			@SuppressWarnings("rawtypes")
			Class util = Class.forName("com.apple.eawt.FullScreenUtilities");
			@SuppressWarnings("rawtypes")
			Class params[] = new Class[] { Window.class, Boolean.TYPE };
			@SuppressWarnings("unchecked")
			Method method = util.getMethod("setWindowCanFullScreen", params);
			method.invoke(util, this, true);
		}
		catch (Exception e)
		{
			System.err.println("Apple full screen utilities not found.");
		}
		changeHistory = new ERChangeHistory();
		changeHistory.setHistoryChangeNotifier(this);
		
		erView = new ERView();
		erView.setSize(3000, 2000);
		erView.setPreferredSize(new Dimension(3000, 2000));
		erView.setMinimumSize(new Dimension(3000, 2000));
		erView.setChangeHistory(changeHistory);
		spERView = new JScrollPane(erView, ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED,
				ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		spERView.setAutoscrolls(true);
		spERView.getVerticalScrollBar().setUnitIncrement(6);
		spERView.getHorizontalScrollBar().setUnitIncrement(6);
		spERView.setBorder(new EmptyBorder(0, 0, 0, 0));
		
		AdjustmentListener adjustmentListener = e ->
		{
			JViewport viewPort = spERView.getViewport();
			erView.setVisibleRect(viewPort.getViewRect());
		};
		spERView.getHorizontalScrollBar().addAdjustmentListener(adjustmentListener);
		spERView.getVerticalScrollBar().addAdjustmentListener(adjustmentListener);
		
		add(spERView);
		
		editor = new EntityEditor();
		erView.setERSelectionNotifier(editor);
		erView.setERSelectionNotifier(layout);
		editor.setRepaintRequest(erView);
		editor.setModelQuery(erView);
		editor.setChangeHistory(changeHistory);
		add(editor);
		
		JMenuBar menuBar = new JMenuBar();
		
		JMenu menuFile = new JMenu();
		menuFile.setText(ER_Editor.LOCALIZATION.getString("file_menu"));
		
		{
			menuNew = new JMenuItem();
			menuNew.setText(ER_Editor.LOCALIZATION.getString("new_diagram"));
			menuNew.setMnemonic('N');
			menuNew.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuNew.addActionListener(this);
			menuFile.add(menuNew);
			
			menuLoadQuiz1 = new JMenuItem();
	        menuLoadQuiz1.setText(ER_Editor.LOCALIZATION.getString("quiz1"));
	        menuLoadQuiz1.setMnemonic('Q');
	        menuLoadQuiz1.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_Q, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
	        menuLoadQuiz1.addActionListener(this);
	        menuFile.add(menuLoadQuiz1);
			menuOpen = new JMenuItem();
			
			menuOpen.setText(ER_Editor.LOCALIZATION.getString("open_diagram"));
			menuOpen.setMnemonic('O');
			menuOpen.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuOpen.addActionListener(this);
			menuFile.add(menuOpen);
			
			menuFile.addSeparator();
			
			menuClose = new JMenuItem();
			menuClose.setText(ER_Editor.LOCALIZATION.getString("close_diagram"));
			menuClose.setMnemonic('W');
			menuClose.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_W, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuClose.addActionListener(this);
			menuFile.add(menuClose);
			//디비에 저장 버튼 구현
			menuSaveToDB = new JMenuItem();
			menuSaveToDB.setText(ER_Editor.LOCALIZATION.getString("save_to_db"));
			menuSaveToDB.setMnemonic('D');
			menuSaveToDB.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_D, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuSaveToDB.addActionListener(this);
			menuFile.add(menuSaveToDB);
			
			//디비에서 불러오기 버튼 구현
			menuLoadFromDB = new JMenuItem();
			menuLoadFromDB.setText(ER_Editor.LOCALIZATION.getString("load_from_db"));
			menuLoadFromDB.setMnemonic('B');
			menuLoadFromDB.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_B, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuLoadFromDB.addActionListener(this);
			menuFile.add(menuLoadFromDB);
			
			menuSave = new JMenuItem();
			menuSave.setText(ER_Editor.LOCALIZATION.getString("save_diagram"));
			menuSave.setMnemonic('S');
			menuSave.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuSave.addActionListener(this);
			menuFile.add(menuSave);
			
			menuSaveAs = new JMenuItem();
			menuSaveAs.setText(ER_Editor.LOCALIZATION.getString("save_diagram_as"));
			menuSaveAs.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_S, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuSaveAs.addActionListener(this);
			menuFile.add(menuSaveAs);
			
			menuFile.addSeparator();
			
			menuExport = new JMenu();
			menuExport.setText(ER_Editor.LOCALIZATION.getString("export_diagram"));
			{
				menuExportImage = new JMenuItem();
				menuExportImage.setText(ER_Editor.LOCALIZATION.getString("export_image"));
				menuExportImage.addActionListener(this);
				menuExport.add(menuExportImage);
				
				menuExportModel = new JMenuItem();
				menuExportModel.setText(ER_Editor.LOCALIZATION.getString("export_model"));
				menuExportModel.addActionListener(this);
				menuExport.add(menuExportModel);
			}
			menuFile.add(menuExport);
		}
		menuBar.add(menuFile);
		
		JMenu menuEdit = new JMenu();
		menuEdit.setText(ER_Editor.LOCALIZATION.getString("edit_menu"));
		
		{
			menuUndo = new JMenuItem();
			menuUndo.setText(ER_Editor.LOCALIZATION.getString("undo"));
			menuUndo.setMnemonic('Z');
			menuUndo.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_Z, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuUndo.addActionListener(this);
			menuUndo.setEnabled(false);
			menuEdit.add(menuUndo);
			
			menuRedo = new JMenuItem();
			menuRedo.setText(ER_Editor.LOCALIZATION.getString("redo"));
			menuRedo.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_Z, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuRedo.addActionListener(this);
			menuRedo.setEnabled(false);
			menuEdit.add(menuRedo);
			
			menuEdit.addSeparator();
			
			menuCut = new JMenuItem();
			menuCut.setText(ER_Editor.LOCALIZATION.getString("cut"));
			menuCut.setMnemonic('X');
			menuCut.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_X, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuCut.setActionCommand((String) TransferHandler.getCutAction().getValue(Action.NAME));
			menuCut.addActionListener(this);
			menuEdit.add(menuCut);
			
			menuCopy = new JMenuItem();
			menuCopy.setText(ER_Editor.LOCALIZATION.getString("copy"));
			menuCopy.setMnemonic('C');
			menuCopy.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_C, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuCopy.setActionCommand((String) TransferHandler.getCopyAction().getValue(Action.NAME));
			menuCopy.addActionListener(this);
			menuEdit.add(menuCopy);
			
			menuPaste = new JMenuItem();
			menuPaste.setText(ER_Editor.LOCALIZATION.getString("paste"));
			menuPaste.setMnemonic('V');
			menuPaste.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_V, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuPaste.setActionCommand((String) TransferHandler.getPasteAction().getValue(Action.NAME));
			menuPaste.addActionListener(this);
			menuEdit.add(menuPaste);
			
			menuEdit.addSeparator();
			
			menuSelectAll = new JMenuItem();
			menuSelectAll.setText(ER_Editor.LOCALIZATION.getString("select_all"));
			menuSelectAll.setMnemonic('A');
			menuSelectAll.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_A, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuSelectAll.addActionListener(this);
			menuEdit.add(menuSelectAll);
			
			menuDelete = new JMenuItem();
			menuDelete.setText(ER_Editor.LOCALIZATION.getString("delete"));
			menuDelete.setMnemonic('D');
			menuDelete.setAccelerator(KeyStroke.getKeyStroke(System.getProperty("os.name").equals("Mac OS X") ? KeyEvent.VK_BACK_SPACE : KeyEvent.VK_DELETE, 0));
			menuDelete.addActionListener(this);
			menuEdit.add(menuDelete);
		}
		menuBar.add(menuEdit);
		
		JMenu menuModel = new JMenu();
		menuModel.setText(ER_Editor.LOCALIZATION.getString("model_menu"));
		
		{
			menuAddEntity = new JMenuItem();
			menuAddEntity.setText(ER_Editor.LOCALIZATION.getString("add_entity"));
			menuAddEntity.setMnemonic('E');
			menuAddEntity.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_E, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuAddEntity.addActionListener(this);
			menuModel.add(menuAddEntity);
			
			menuAddRelationship = new JMenuItem();
			menuAddRelationship.setMnemonic('R');
			menuAddRelationship.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_R, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuAddRelationship.setText(ER_Editor.LOCALIZATION.getString("add_relationship"));
			menuAddRelationship.addActionListener(this);
			menuModel.add(menuAddRelationship);

			menuAddDescriptionBox = new JMenuItem();
			menuAddDescriptionBox.setText(ER_Editor.LOCALIZATION.getString("add_description_box"));
			menuAddDescriptionBox.setMnemonic('D');
			menuAddDescriptionBox.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_D, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuAddDescriptionBox.addActionListener(this);
			menuModel.add(menuAddDescriptionBox);
		}
		menuBar.add(menuModel);
		
		JMenu menuView = new JMenu();
		menuView.setText(ER_Editor.LOCALIZATION.getString("view_menu"));
		
		{
			menuZoomOriginal = new JMenuItem();
			menuZoomOriginal.setText(ER_Editor.LOCALIZATION.getString("zoom_original"));
			menuZoomOriginal.setMnemonic('0');
			menuZoomOriginal.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_0, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuZoomOriginal.addActionListener(this);
			menuView.add(menuZoomOriginal);
			
			menuZoomOut = new JMenuItem();
			menuZoomOut.setText(ER_Editor.LOCALIZATION.getString("zoom_out"));
			menuZoomOut.setMnemonic('-');
			menuZoomOut.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_MINUS, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuZoomOut.addActionListener(this);
			menuView.add(menuZoomOut);
			
			menuZoomIn = new JMenuItem();
			menuZoomIn.setText(ER_Editor.LOCALIZATION.getString("zoom_in"));
			menuZoomIn.setMnemonic('+');
			menuZoomIn.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_PLUS, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask()));
			menuZoomIn.addActionListener(this);
			menuView.add(menuZoomIn);
			
			menuImplode = new JMenuItem();
			menuImplode.setText(ER_Editor.LOCALIZATION.getString("compress"));
			menuImplode.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_MINUS, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuImplode.addActionListener(this);
			menuView.add(menuImplode);
			
			menuExpand = new JMenuItem();
			menuExpand.setText(ER_Editor.LOCALIZATION.getString("expand"));
			menuExpand.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_PLUS, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuExpand.addActionListener(this);
			menuView.add(menuExpand);
		}
		menuBar.add(menuView);
		
		JMenu menuHelp = new JMenu();
		menuHelp.setText(ER_Editor.LOCALIZATION.getString("help_menu"));
		
		{
			menuItemHelp = new JMenuItem();
			menuItemHelp.setText(ER_Editor.LOCALIZATION.getString("show_help"));
			menuItemHelp.setMnemonic('H');
			menuItemHelp.setAccelerator(
					KeyStroke.getKeyStroke(KeyEvent.VK_H, Toolkit.getDefaultToolkit().getMenuShortcutKeyMask() | Event.SHIFT_MASK));
			menuItemHelp.addActionListener(this);
			menuHelp.add(menuItemHelp);
			
			menuItemAbout = new JMenuItem();
			menuItemAbout.setText(ER_Editor.LOCALIZATION.getString("about_menu"));
			menuItemAbout.setMnemonic('A');
			menuItemAbout.addActionListener(this);
			menuHelp.add(menuItemAbout);
		}
		menuBar.add(menuHelp);
		
		setJMenuBar(menuBar);
		setTitle("Recursive_ER");
		ER_Editor.openedFrames.add(this);
		setVisible(true);
	}

	@Override
	public void itemStateChanged(ItemEvent e) {
		// TODO Auto-generated method stub
		
	}
}
