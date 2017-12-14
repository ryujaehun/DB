
/**
  * EReditor
  * ERModel.java
  * Created by Palle on 14.05.2014
  * Copyright (c) 2014 - 2017 Palle.
  *
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

import java.awt.Color;
import java.awt.FileDialog;
import java.awt.Graphics2D;
import java.awt.Rectangle;
import java.awt.RenderingHints;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import javax.imageio.ImageIO;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JOptionPane;

//DB에 저장할때 이쪽 수정하면 될듯
public class ERModel
{
	
	ResultSet rs = null;
	@SuppressWarnings("unchecked")
	//저장된 객체들 불러옴
	public static ERModel open(File f) throws IOException, ClassNotFoundException
	{
		FileInputStream fis = new FileInputStream(f);
		ObjectInputStream ois = new ObjectInputStream(fis);
		ERModel model = new ERModel();
		model.entities = (List<Entity>) ois.readObject();
		model.relationships = (List<Relationship>) ois.readObject();
		model.descriptions = (List<DescriptionBox>) ois.readObject();
		model.fileName = f;
		model.saved = true;
		ois.close();
		return model;
	}
	
	public static ERModel open(JFrame parentFrame)
	{
		FileDialog dialog = new FileDialog(parentFrame, "ER-Modelldatei ausw\u00e4hlen...", FileDialog.LOAD);
		dialog.setFile("*.erm");
		dialog.setFilenameFilter((dir, name) -> name.endsWith(".erm"));
		dialog.setVisible(true);
		String filename = dialog.getFile();
		String dir = dialog.getDirectory();
		if (filename != null)
		{
			try
			{
				return open(new File(dir + filename));
			}
			catch (ClassNotFoundException | IOException e)
			{
				JOptionPane.showMessageDialog(null, ER_Editor.LOCALIZATION.getString("open_error_message") + "\n" + e.getLocalizedMessage(),
						ER_Editor.LOCALIZATION.getString("open_error_title"), JOptionPane.ERROR_MESSAGE);
			}
		}
		return null;
	}
	
	protected List<Entity>              entities;
	protected List<Relationship>   relationships;
	protected List<DescriptionBox> descriptions;
	
	protected File fileName;
	
	
	public void ModelClear() {
	      entities.clear();
	      relationships.clear();
	      descriptions.clear();
	}
	
	public int checkModelwithQuiz(int quiz_id) throws SQLException {
		float rate =0 ;
		ConnTibero ct =new ConnTibero();
		ct.connect();
		int ansCount=0;				//정답 개체 카운트 개수
		int relationshipCount =0;	//정답 relationship 개수
		int entityCount =0;			//정답 entity 개수
		int totCount;				//총 객체 수
		
		ArrayList<Entity> ansEntities = new ArrayList<Entity>();			//정답 엔티티
		ArrayList<Relationship> ansRelationships = new ArrayList<Relationship>();	//정답 릴레이션
		
		//정답 엔티티 세팅
		rs = ct.excute("SELECT e.entity_id, o.x, o.y, o.width, o.height, o.name, o.weak FROM schema s, erobject o, entity e WHERE s.schema_id = o.schema_id AND o.object_id = e.object_id AND o_type = 1 AND s.schema_id="+ quiz_id+";");
		while(rs.next()) {
			int e_id = rs.getInt(1);
			Entity newEn = new Entity();
			newEn.setBounds(new Rectangle(rs.getInt(2), rs.getInt(3), rs.getInt(4), rs.getInt(5)));//바운드 설정
			newEn.setName(rs.getString(6));
			if(rs.getInt(7)==1) {
				newEn.setWeak(true);
			}
			else {
				newEn.setWeak(false);
			}
			newEn.setE_id(e_id);
			ResultSet rs2 =ct.excute("SELECT a.name, a.iskey FROM attribute a, entity e WHERE a.entity_id = e.entity_id AND e.entity_id = " + e_id + ";");
			ArrayList<Attribute> tempAtts = new ArrayList<Attribute>();
			//엔티티에 연결된 어트리 뷰트 구함
			while(rs2.next()) {
				Attribute newAt = new Attribute();
				newAt.setName(rs2.getString(1));
				if(rs2.getInt(2)==1) {
					newAt.setKeyAttribute(true);
				}
				else {
					newAt.setKeyAttribute(false);
				}
				tempAtts.add(newAt);
			}
			newEn.setAttributes(tempAtts);
			ansEntities.add(newEn);
		}
		
		//정답 릴레이션 세팅
		rs = ct.excute("SELECT o.x, o.y, o.width, o.height, o.name, o.weak, r.relationship_id FROM schema s, erobject o , relationship r WHERE s.schema_id = o.schema_id AND o.object_id = r.object_id AND o_type=2 AND s.schema_id="+ quiz_id+";");
		while(rs.next()) {
			Relationship re = new Relationship();
			re.setBounds(new Rectangle(rs.getInt(1), rs.getInt(2), rs.getInt(3), rs.getInt(4)));
			re.setName(rs.getString(5));
			if(rs.getInt(6)==1) {
				re.setWeak(true);
			}
			else {
				re.setWeak(false);
			}
			ResultSet rs2;
			rs2 = ct.excute("SELECT entity_id, etomany FROM relationship_entity WHERE relationship_id = " + rs.getInt(7)+";");
			while(rs2.next()) {                             
				for(Entity en : ansEntities) {
					if(en.getE_id() == rs2.getInt(1)) {
						if(re.getFirstEntity()==null) {
							re.setFirstEntity(en);
							re.setFirstEntityToMany(rs2.getBoolean(2));
						}
						else if(re.getSecondEntity()==null) {
							re.setSecondEntity(en);
							re.setSecondEntityToMany(rs2.getBoolean(2));
						}
					}
					System.out.println("엔티디 하나 추가");
				}
			}
			ansRelationships.add(re);
		}
		
		//엔티티 정답 개수 체크(맞으면 +1, 틀리면 -1)
		boolean equal =false;
		for(Entity en : entities) {
			equal =false;
			for(Entity en2 : ansEntities) {
				if(en.getName().equals(en2.getName()) && en.isWeak() == en2.isWeak() && en.checkAttribute(en2.getAttributes())) {
					equal =true;
					break;
				}
			}
			if(equal != true) {
				ansCount --;
			} else {
				ansCount ++;
			}
		}
		System.out.println(ansCount);
		//릴레이션 정답 개수 체크
		boolean equal2 =false;
		for(Relationship re : relationships) {
			equal =false;
			for(Relationship re2 : ansRelationships) {
				if(re2.getName().equals(re2.getName()) && re.isWeak() == re2.isWeak() && re.checkEntity(re2)) {
					equal =true;
					break;
				}
			}
			if(equal != true) {
				ansCount --;
			} else {
				ansCount ++;
			}
		}
		
		relationshipCount = ansRelationships.size();
		entityCount = ansEntities.size();
		totCount=relationshipCount+entityCount;
		rate = ((float)ansCount/(float)totCount)*100;
		return (int)rate; 
	}
	
	
	private boolean saved;
	//DB에 저장시 얘네들 활용하는 함수 만들어서 저장 아마 커넥트 변수도 여기서 만들어야할듯? -SW
	public ERModel()
	{
		entities = new ArrayList<>();
		relationships = new ArrayList<>();
		descriptions = new ArrayList<>();
	}
	//DB에서 불러오기 -SW
		public void LoadFromDB(int SchemaId) throws SQLException {
			entities.clear();
			relationships.clear();
			ConnTibero ct = new ConnTibero();
			ct.connect();
			//엔티티 먼저 구함
			rs = ct.excute("SELECT e.entity_id, o.x, o.y, o.width, o.height, o.name, o.weak FROM schema s, erobject o, entity e WHERE s.schema_id = o.schema_id AND o.object_id = e.object_id AND o_type = 1 AND s.schema_id="+ SchemaId+";");
			while(rs.next()) {
				int e_id = rs.getInt(1);
				Entity newEn = new Entity();
				newEn.setBounds(new Rectangle(rs.getInt(2), rs.getInt(3), rs.getInt(4), rs.getInt(5)));//바운드 설정
				newEn.setName(rs.getString(6));
				if(rs.getInt(7)==1) {
					newEn.setWeak(true);
				}
				else {
					newEn.setWeak(false);
				}
				newEn.setE_id(e_id);
				ResultSet rs2 =ct.excute("SELECT a.name, a.iskey FROM attribute a, entity e WHERE a.entity_id = e.entity_id AND e.entity_id = " + e_id + ";");
				ArrayList<Attribute> tempAtts = new ArrayList<Attribute>();
				//엔티티에 연결된 어트리 뷰트 구함
				while(rs2.next()) {
					Attribute newAt = new Attribute();
					newAt.setName(rs2.getString(1));
					if(rs2.getInt(2)==1) {
						newAt.setKeyAttribute(true);
					}
					else {
						newAt.setKeyAttribute(false);
					}
					tempAtts.add(newAt);
				}
				newEn.setAttributes(tempAtts);
				entities.add(newEn);
			}
			
			//릴레이션 구함
			rs = ct.excute("SELECT o.x, o.y, o.width, o.height, o.name, o.weak, r.relationship_id FROM schema s, erobject o , relationship r WHERE s.schema_id = o.schema_id AND o.object_id = r.object_id AND o_type=2 AND s.schema_id="+ SchemaId+";");
			while(rs.next()) {
				Relationship re = new Relationship();
				re.setBounds(new Rectangle(rs.getInt(1), rs.getInt(2), rs.getInt(3), rs.getInt(4)));
				re.setName(rs.getString(5));
				if(rs.getInt(6)==1) {
					re.setWeak(true);
				}
				else {
					re.setWeak(false);
				}
				ResultSet rs2;
				rs2 = ct.excute("SELECT entity_id, etomany FROM relationship_entity WHERE relationship_id = " + rs.getInt(7)+";");
				while(rs2.next()) {                             
					for(Entity en : entities) {
						if(en.getE_id() == rs2.getInt(1)) {
							if(re.getFirstEntity()==null) {
								re.setFirstEntity(en);
								re.setFirstEntityToMany(rs2.getBoolean(2));
							}
							else if(re.getSecondEntity()==null) {
								re.setSecondEntity(en);
								re.setSecondEntityToMany(rs2.getBoolean(2));
							}
						}
						System.out.println("엔티디 하나 추가");
					}
				}
				relationships.add(re);
			}
			
			
			
		}
	
	public int boolToInt(boolean bool) {
		if(bool)
			return 1;
		else
			return 0;
	}
	
	//DB에 저장하기 -SW
	public void SaveToDB(String schema_name) throws SQLException {
		ConnTibero ct = new ConnTibero();
		ct.connect();
		String str = null;
		ct.excute("INSERT INTO schema VALUES(SCHEMA_KEY.nextVal,'" + schema_name + "');");
		rs = ct.excute("SELECT SCHEMA_KEY.CURRVAL FROM DUAL;");

		rs.next();
		System.out.println(rs.getInt(1));
		int s_id = rs.getInt(1);
		int o_id, e_id, r_id;			//o_id, e_id, r_id
		for(Entity en : entities) {
			ct.excute("INSERT INTO erobject VALUES (EROBJECT_KEY.nextVal, " + en.getLocation().getX() + "," + en.getLocation().getY() + "," + en.getBounds().getWidth() + "," +  en.getBounds().getHeight()+ ",'" + en.getName() + "'," + boolToInt(en.isWeak()) + ",1," + s_id +");");
			System.out.println("왔니");
			rs = ct.excute("SELECT EROBJECT_KEY.CURRVAL FROM DUAL;");
			rs.next();
			o_id = rs.getInt(1); // o_id의 현재 값을 받음
			ct.excute("INSERT INTO entity VALUES (ENTITY_KEY.nextVal, " + o_id + ");");
			rs = ct.excute("SELECT ENTITY_KEY.CURRVAL FROM DUAL;");
			rs.next();
			e_id = rs.getInt(1);
			en.setE_id(e_id);	//e_id 설정
			for(Attribute at : en.getAttributes()) {
				ct.excute("INSERT INTO attribute VALUES (ATTRIBUTE_KEY.nextVal, " + e_id + ",'" + at.getName() + "'," + boolToInt(at.isKeyAttribute())+ ");");
			}
		}
		
		for(Relationship re : relationships) {
			 {
				ct.excute("INSERT INTO erobject VALUES (EROBJECT_KEY.nextval, " + re.getLocation().getX() + "," + re.getLocation().getY() + "," + re.getBounds().getWidth() + "," + re.getBounds().getHeight() + ",'" + re.getName() + "'," + boolToInt(re.isWeak()) + ", 2, " +s_id + ");");
				rs = ct.excute("SELECT EROBJECT_KEY.CURRVAL FROM DUAL;");
				rs.next();
				o_id = rs.getInt(1);
				ct.excute("INSERT INTO relationship VALUES(RELATIONSHIP_KEY.nextVal," + o_id + ");");
				rs = ct.excute("SELECT RELATIONSHIP_KEY.CURRVAL FROM DUAL;");
				rs.next();
				r_id = rs.getInt(1);
//				boolean tomany;
//				if(re.getFirstEntity()==en)
//					tomany = re.getFirstEntityToMany();
//				else
//					tomany = re.getSecondEntityToMany();
				ct.excute("INSERT INTO RELATIONSHIP_ENTITY VALUES(" + r_id + "," + re.getFirstEntity().getE_id() + "," + boolToInt(re.getFirstEntityToMany()) + ");");
				ct.excute("INSERT INTO RELATIONSHIP_ENTITY VALUES(" + r_id + "," + re.getSecondEntity().getE_id() + "," + boolToInt(re.getSecondEntityToMany()) + ");");
			}
		}
		//INSERT
		System.out.println("SQL 끝!!!");
	}

	public void addEntity()
	{
		for (Entity e1 : entities)
			e1.deselect();
		entities.add(new Entity());
	}
	
	public void export()
	{
		JFileChooser chooser = new JFileChooser();
		if (chooser.showSaveDialog(null) == JFileChooser.APPROVE_OPTION)
		{
			File f = chooser.getSelectedFile();
			try
			{
				ImageIO.write(render(), "png", f);
			}
			catch (IOException e)
			{
				JOptionPane.showMessageDialog(null, ER_Editor.LOCALIZATION.getString("image_export_error_message"),
						ER_Editor.LOCALIZATION.getString("image_export_error_title"), JOptionPane.ERROR_MESSAGE);
			}
		}
	}
	
	public void exportModel()
	{
		ERModelConverter converter = new ERModelConverter(this);
		converter.convert();
		converter.display();
	}
	
	public String getFilename()
	{
		if (saved)
			return fileName.getName();
		else
			return ER_Editor.LOCALIZATION.getString("model_name");
	}
	
	public boolean isEmpty()
	{
		return entities.isEmpty() && relationships.isEmpty() && descriptions.isEmpty() && !saved;
	}
	
	public void layoutBoxes()
	{
	
	}
	
	public boolean needsSave()
	{
		if (!saved)
			return true;
		else
		{
			try
			{
				return !open(fileName).equals(this);
			}
			catch (ClassNotFoundException e)
			{
				e.printStackTrace();
			}
			catch (IOException e)
			{
				e.printStackTrace();
			}
		}
		return true;
	}
	
	public BufferedImage render()
	{
		int maxX = 0;
		int maxY = 0;
		
		for (Relationship r : relationships)
		{
			Rectangle bounds = r.bounds;
			maxX = (int) Math.max(maxX, bounds.getMaxX() + 200);
			maxY = (int) Math.max(maxY, bounds.getMaxY() + 200);
		}
		
		for (Entity e : entities)
		{
			Rectangle bounds = e.bounds;
			maxX = (int) Math.max(maxX, bounds.getMaxX() + 200);
			maxY = (int) Math.max(maxY, bounds.getMaxY() + 200);
		}
		
		BufferedImage image = new BufferedImage(maxX * 2, maxY * 2, BufferedImage.TYPE_INT_RGB);
		Graphics2D g = image.createGraphics();
		g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
		g.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
		g.setColor(Color.WHITE);
		g.fillRect(0, 0, maxX * 2, maxY * 2);
		g.scale(2.0f, 2.0f);
		
		for (Relationship r : relationships)
		{
			r.deselect();
			r.paint(g);
		}
		for (Entity e : entities)
		{
			e.deselect();
			e.paint(g);
		}
		
		g.dispose();
		return image;
	}
	
	public boolean save(JFrame parentFrame)
	{
		boolean success = false;
		if (!saved)
			success = saveAs(true, parentFrame);
		else
			success = save(fileName);
		saved = saved || success;
		return success;
	}
	
	public boolean saveAs(JFrame parentFrame)
	{
		return saveAs(true, parentFrame);
	}
	
	public boolean saveAsCopy(JFrame parentFrame)
	{
		return saveAs(false, parentFrame);
	}
	
	private boolean save(File f)
	{
		if (!f.getAbsolutePath().endsWith(".erm"))
			f = new File(f.getAbsolutePath() + ".erm");
		try
		{
			FileOutputStream fos = new FileOutputStream(f);
			ObjectOutputStream oos = new ObjectOutputStream(fos);
			oos.writeObject(entities);
			oos.writeObject(relationships);
			oos.writeObject(descriptions);
			oos.close();
			// XMLEncoder encoder = new XMLEncoder(new BufferedOutputStream(new
			// FileOutputStream(f)));
			// encoder.writeObject(entities);
			// encoder.writeObject(relationships);
			// encoder.close();
			return true;
		}
		catch (FileNotFoundException e)
		{
			JOptionPane.showMessageDialog(null, ER_Editor.LOCALIZATION.getString("save_error_message") + "\n" + e.getLocalizedMessage(),
					ER_Editor.LOCALIZATION.getString("save_error_title"), JOptionPane.ERROR_MESSAGE);
			return false;
		}
		catch (IOException e)
		{
			JOptionPane.showMessageDialog(null, ER_Editor.LOCALIZATION.getString("save_error_message") + "\n" + e.getLocalizedMessage(),
					ER_Editor.LOCALIZATION.getString("save_error_title"), JOptionPane.ERROR_MESSAGE);
			return false;
		}
		
	}
	
	private boolean saveAs(boolean keepNewFileName, JFrame parent)
	{
		FileDialog dialog = new FileDialog(parent, ER_Editor.LOCALIZATION.getString("save_as"), FileDialog.SAVE);
		dialog.setVisible(true);
		String filename = dialog.getFile();
		String dir = dialog.getDirectory();
		if (filename != null)
		{
			if (keepNewFileName)
				fileName = new File(dir + filename);
			return save(new File(dir + filename));
		}
		return false;
	}
}
