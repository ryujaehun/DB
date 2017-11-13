
#pragma once
#include "stdafx.h"

enum SHAPEID{QUAD, CIRCLE, LINE};
enum MOUSEEVENT{
	MOVE, DRAG, LBUTTON_DOWN, LBUTTON_UP, RBUTTONDOWN, RBUTTON_UP};

class Shape: public CObject
{
DECLARE_DYNAMIC(Shape)
public:
	Shape() : m_start(0) { m_shapeType = (SHAPEID)shapeType; m_vertex[0] = m_vertex[1] = CPoint(0, 0); width = 2; };

	void Serialize(CArchive& ar);
	int IsCompleted(){if(m_start==-1) return 1; else return 0;}
	void SetStart(){m_start=1;}
	int IsStarted(){if(m_start>0) return 1; else return 0;}
	void NormalEnd(){m_start=-1;}

	void SaveToFile(CFile &fout);
	void LoadFromFile(CFile &fin);
	void incWidth();
	void decWidth();

	virtual void DisplaySelected(CDC* pDC)=0;
	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags)=0;
	virtual void Display(CDC* pDC)=0;
	virtual void MoveShape(UINT nChar)=0;

	CPoint returnPoint1(){return m_vertex[0];}
	CPoint returnPoint2(){return m_vertex[1];}

protected:
	SHAPEID m_shapeType;
	int shapeType;
	int m_start;
	CPoint  m_vertex[2];
	UINT m_flags;
	int width;

};

