
// ChildView.h : interface of the CChildView class
//


#pragma once
#include "Quad.h"
#include "Circle.h"
#include "Line.h"
// CChildView window

class CChildView : public CWnd
{
// Construction
public:
	CChildView();

// Attributes
public:

// Operations
public:

// Overrides
	protected:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

// Implementation
public:
	virtual ~CChildView();

	// Generated message map functions
protected:
	afx_msg void OnPaint();
	DECLARE_MESSAGE_MAP()

	CList<Shape*>m_shapeList;
	Shape* m_newShape;
	SHAPEID m_shapeType;
	Shape* selectedShape;

public:
//	afx_msg void OnLButtonDblClk(UINT nFlags, CPoint point);

protected:

	
	CPoint m_pos[2];
	CPoint m_prePtr;
	CString str2;
	

public:
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);//마우스가 움직일때 이벤트 핸들러
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);//마우스가 왼쪽버튼이 눌릴때 이벤트 핸들러
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);//마우스 왼쪽버튼이 올라갈때 이벤트 핸들러
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	Shape* allocShape();//도형의 모양을 정하는 함수.m_newShape에 따라 동적할당을 함.
	afx_msg void OnShapeQuad();//기존의 m_newShape를 지우고 m_shapeType을 QUAD로 바꿈
	afx_msg void OnShapeCircle();//기존의 m_newShape를 지우고 m_shapeType을 CIRCLE로 바꿈
	afx_msg void OnShapeLine();//기존의 m_newShape를 지우고 m_shapeType을 LINE으로 바꿈
	afx_msg void OnUpdateShapeQuad(CCmdUI *pCmdUI);//	m_shapeType을 QUAD로 바꿈
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	afx_msg void OnContextMenu(CWnd* /*pWnd*/, CPoint /*point*/);//메뉴를 만드는 함수, 우클릭시 모양을 바꾸는 매뉴를 사용할수있게함.


	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnFileSave32775();
	afx_msg void OnFileLoad();
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
};

