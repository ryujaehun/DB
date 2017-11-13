
// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "WP_lab07.h"
#include "ChildView.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define pi 3.141592653589793238462643383279f
// CChildView

CChildView::CChildView(){
	//�ɹ��������� �ʱ�ȭ��.
	m_pos[0].x=0;
	m_pos[0].y=0;
	m_pos[1].x=0;
	m_pos[1].y=0;
	m_newShape=NULL;
	m_shapeType =QUAD;
	selectedShape=m_newShape;
}

CChildView::~CChildView()
{
	//�������� �����ϴ� ����Ʈ�� �Ҹ��Ŵ.
	while(m_shapeList.GetSize()){
		Shape* shape=m_shapeList.RemoveHead();
		delete shape;
	}
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_WM_MOUSEMOVE()
	ON_WM_LBUTTONDOWN()
	ON_WM_KEYDOWN()


	ON_WM_CONTEXTMENU()
	ON_WM_LBUTTONUP()
	ON_COMMAND(ID_SHAPE_CIRCLE, &CChildView::OnShapeCircle)
	ON_COMMAND(ID_SHAPE_LINE, &CChildView::OnShapeLine)
	ON_COMMAND(ID_SHAPE_QUAD, &CChildView::OnShapeQuad)
	ON_WM_KEYDOWN()
	ON_COMMAND(ID_FILE_SAVE32775, &CChildView::OnFileSave32775)
	ON_COMMAND(ID_FILE_LOAD, &CChildView::OnFileLoad)
	ON_WM_MOUSEWHEEL()
END_MESSAGE_MAP()





BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting

	POSITION pos= m_shapeList.GetHeadPosition();//����Ʈ�� �Ǿ� �ڸ��� ����.
	for(int i=0; i<m_shapeList.GetSize(); i++){
		Shape* shape=m_shapeList.GetNext(pos);
		shape->Display(&dc);	
	}//�ݺ����� ���� ����Ʈ�� �������� �׷���
	CString str;
	str.Format( "Current mouse: [%d  ,%d]", m_pos[1].x, m_pos[1].y);
	dc.TextOut(10, 20,str);//str�� ȭ�鿡 �����.
	if(m_newShape){
		m_newShape->Display(&dc);
	}
		if(selectedShape!=NULL){
			selectedShape->Display(&dc);
			selectedShape->DisplaySelected(&dc);
		}
		dc.TextOut(10,40, str2);
}


void CChildView::OnMouseMove(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default	
	m_pos[1]=point;
	if(m_pos[1].x<m_pos[0].x&&m_pos[1].y<m_pos[0].y){
		CPoint temp;
		temp=m_pos[1];
		m_pos[1]=m_pos[0];
		m_pos[0]=temp;
	}
	CClientDC dc(this);
	if(m_newShape) {
		m_newShape->Draw(MOVE, point.x, point.y, nFlags);//������ �׸�
		str2.Format("rect: [%d  ,%d] ~ [%d  ,%d]", m_pos[0].x, m_pos[0].y, m_pos[1].x, m_pos[1].y);//str2(2��° ��)�� ������ ������.
		Invalidate();
	}

	CWnd::OnMouseMove(nFlags, point);
} 

void CChildView::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default	
	if(!m_newShape) {
		m_pos[0]=point;
		m_newShape = this->allocShape();//���� ���¸� ������.
		m_newShape->Draw(LBUTTON_DOWN, point.x, point.y, nFlags);//������ �׸�
			Invalidate();
	}
	CClientDC dc(this);
	POSITION pos= m_shapeList.GetHeadPosition();
	for(int i=0; i<m_shapeList.GetSize(); i++){
		Shape* shape=m_shapeList.GetNext(pos);

		if(point.x>=shape->returnPoint1().x&&point.x<=shape->returnPoint2().x&&point.y>=shape->returnPoint1().y&&point.y<=shape->returnPoint2().y){
			selectedShape=shape;
			break;
		}
	}
	CWnd::OnLButtonDown(nFlags, point);
}



void CChildView::OnUpdateShapeQuad(CCmdUI *pCmdUI)
{
	pCmdUI->SetCheck(m_shapeType == QUAD);
}


Shape* CChildView::allocShape() {
	//���� m_newShape�� ������ ����
	if(m_newShape) {
		delete m_newShape;
	}
	//m_shapeType������ �����Ҵ��� ��.
	switch(m_shapeType) {
	case QUAD:
		return new Quad();
		break;
	case LINE:
		return new Line();
		break;
	case CIRCLE:
		return new Circle();
		break;
	default:
		return NULL;
		break;
	}
}


void CChildView::OnContextMenu(CWnd* pWnd, CPoint point)
{
	// TODO: Add your message handler code here
	CMenu menu;
	menu.LoadMenu(IDR_MAINFRAME);
	CMenu *pMenu= menu.GetSubMenu(4);
	pMenu->TrackPopupMenu(TPM_LEFTALIGN|TPM_RIGHTBUTTON,point.x, point.y, AfxGetMainWnd());
}


void CChildView::OnLButtonUp(UINT nFlags, CPoint point)
{
	if(m_newShape) {
		m_shapeList.AddTail(m_newShape);//����Ʈ�� �� ������ �߰���.
		m_newShape = NULL;//�׷��� ������ NULL�� ����
	}
	// TODO: Add your message handler code here and/or call default
	str2.Empty();
	Invalidate();
	CWnd::OnLButtonUp(nFlags, point);
}


void CChildView::OnShapeCircle()
{
	// TODO: Add your command handler code here
		if(m_newShape) {
		delete m_newShape;
		m_newShape = NULL;
	}
	m_shapeType = CIRCLE;
}


void CChildView::OnShapeLine()
{
	// TODO: Add your command handler code here
		if(m_newShape) {
		delete m_newShape;
		m_newShape = NULL;
	}
	m_shapeType = LINE;
}

void CChildView::OnShapeQuad()
{
	if(m_newShape) {
		delete m_newShape;
		m_newShape = NULL;
	}
	m_shapeType = QUAD;
}

void CChildView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	// TODO: Add your message handler code here and/or call default
	
	if(selectedShape!=NULL){
	selectedShape->MoveShape(nChar);
	Invalidate();
	}
	CWnd::OnKeyDown(nChar, nRepCnt, nFlags);
}


void CChildView::OnFileSave32775()
{
	CFileDialog dlg(FALSE);
	if(dlg.DoModal()==IDOK){
		MessageBox(dlg.GetPathName());
		CFile savefile;
		CFileException e;
		if(!savefile.Open(dlg.GetPathName(), CFile::modeWrite|CFile::modeCreate ,&e)){
			e.ReportError();
			return;
		}
		POSITION pos= m_shapeList.GetHeadPosition();//����Ʈ�� �Ǿ� �ڸ��� ����.
		for(int i=0; i<m_shapeList.GetSize(); i++){
			Shape* shape=m_shapeList.GetNext(pos);
			shape->SaveToFile(savefile);
		}
	}
	// TODO: Add your command handler code here
}


void CChildView::OnFileLoad()
{
	int count, i, length;
	CFileDialog dlg(TRUE);
	if(dlg.DoModal()==IDOK){
		MessageBox(dlg.GetPathName());
		CFile loadfile;
		CFileException e;
		if(!loadfile.Open(dlg.GetPathName(), CFile::modeRead,&e)){
			e.ReportError();
			return;
		}
		
		POSITION pos=m_shapeList.GetHeadPosition();
		{
		m_newShape->LoadFromFile(loadfile);
		m_shapeList.AddTail(m_newShape);
		}
	}


	// TODO: Add your command handler code here

}


BOOL CChildView::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt)
{
	// TODO: ���⿡ �޽��� ó���� �ڵ带 �߰� ��/�Ǵ� �⺻���� ȣ���մϴ�.
	if (WM_MOUSEWHEEL&&selectedShape!=nullptr) {
		if (zDelta > 0)
			selectedShape->incWidth();
		else
			selectedShape->decWidth();
	}
	Invalidate();
	return CWnd::OnMouseWheel(nFlags, zDelta, pt);
}
