
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
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);//���콺�� �����϶� �̺�Ʈ �ڵ鷯
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);//���콺�� ���ʹ�ư�� ������ �̺�Ʈ �ڵ鷯
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);//���콺 ���ʹ�ư�� �ö󰥶� �̺�Ʈ �ڵ鷯
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	Shape* allocShape();//������ ����� ���ϴ� �Լ�.m_newShape�� ���� �����Ҵ��� ��.
	afx_msg void OnShapeQuad();//������ m_newShape�� ����� m_shapeType�� QUAD�� �ٲ�
	afx_msg void OnShapeCircle();//������ m_newShape�� ����� m_shapeType�� CIRCLE�� �ٲ�
	afx_msg void OnShapeLine();//������ m_newShape�� ����� m_shapeType�� LINE���� �ٲ�
	afx_msg void OnUpdateShapeQuad(CCmdUI *pCmdUI);//	m_shapeType�� QUAD�� �ٲ�
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	afx_msg void OnContextMenu(CWnd* /*pWnd*/, CPoint /*point*/);//�޴��� ����� �Լ�, ��Ŭ���� ����� �ٲٴ� �Ŵ��� ����Ҽ��ְ���.


	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnFileSave32775();
	afx_msg void OnFileLoad();
	afx_msg BOOL OnMouseWheel(UINT nFlags, short zDelta, CPoint pt);
};

