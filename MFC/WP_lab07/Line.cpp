#include "stdafx.h"
#include "Line.h"

Line:: Line(){
	m_shapeType=LINE;
}



void Line::Draw(MOUSEEVENT ev, int x, int y, UINT nFlags){
		m_flags=nFlags;
	switch(ev){
	case LBUTTON_DOWN:
		if(!IsStarted()){
			setLeftTopPoint(x,y);
			setRightBottomPoint(x,y);
			SetStart();
		}
		break;
	case MOVE:
		if(IsStarted()){
			setRightBottomPoint(x,y);
		}
		break;
	default:
		break;
	}
}

void Line::setLeftTopPoint(int x, int y){
	m_vertex[0]=CPoint(x,y);
}

void Line::setRightBottomPoint(int x, int y){
	m_vertex[1]= CPoint(x,y);
}

void Line::DisplaySelected(CDC* pDC){
	CPen pen;
	pen.CreatePen(PS_DOT, 1, RGB(0, 0,0));
	CPen* old=pDC->SelectObject(&pen);

	CPoint point[4];
	pDC->MoveTo(m_vertex[0].x, m_vertex[0].y);
	pDC->LineTo(m_vertex[0].x, m_vertex[1].y);
	pDC->LineTo(m_vertex[1].x, m_vertex[1].y);
	pDC->LineTo(m_vertex[1].x, m_vertex[0].y);
	pDC->LineTo(m_vertex[0].x, m_vertex[0].y);

	pDC->SelectObject(old);
	pDC->Rectangle(m_vertex[0].x-4,m_vertex[0].y-4, m_vertex[0].x+4,m_vertex[0].y+4);
	pDC->Rectangle(m_vertex[1].x-4,m_vertex[1].y-4, m_vertex[1].x+4,m_vertex[1].y+4);
	pDC->Rectangle(m_vertex[1].x-4,m_vertex[0].y-4, m_vertex[1].x+4,m_vertex[0].y+4);
	pDC->Rectangle(m_vertex[0].x-4,m_vertex[1].y-4, m_vertex[0].x+4,m_vertex[1].y+4);
	pDC->Rectangle(abs(m_vertex[0].x+m_vertex[1].x)/2-4,m_vertex[0].y-4, abs(m_vertex[0].x+m_vertex[1].x)/2+4, m_vertex[0].y+4);
	pDC->Rectangle(abs(m_vertex[0].x+m_vertex[1].x)/2-4,m_vertex[1].y-4, abs(m_vertex[0].x+m_vertex[1].x)/2+4, m_vertex[1].y+4);
	pDC->Rectangle(m_vertex[0].x-4,abs(m_vertex[0].y+m_vertex[1].y)/2-4, m_vertex[0].x+4, abs(m_vertex[0].y+m_vertex[1].y)/2+4);
	pDC->Rectangle(m_vertex[1].x-4,abs(m_vertex[0].y+m_vertex[1].y)/2-4, m_vertex[1].x+4, abs(m_vertex[0].y+m_vertex[1].y)/2+4);
	
}
void Line::MoveShape(UINT nChar){
	if(WM_KEYDOWN){
		switch(nChar){
		case VK_UP:
			m_vertex[0].y-=1;
			m_vertex[1].y-=1;
			break;
		case VK_DOWN:
			m_vertex[0].y+=1;
			m_vertex[1].y+=1;
			break;
		case VK_LEFT:
			m_vertex[0].x-=1;
			m_vertex[1].x-=1;
			break;
		case VK_RIGHT:
			m_vertex[0].x+=1;
			m_vertex[1].x+=1;
			break;
		default:
			break;
		}
	}
}
void Line::Display(CDC* pDC){
	CPen pen;
	pen.CreatePen(PS_DOT, width, RGB(0, 0, 0));
	CPen* old = pDC->SelectObject(&pen);

	if( m_flags & MK_SHIFT ) {
		m_vertex[1].y = m_vertex[0].y + (m_vertex[1].x - m_vertex[0].x);
	}
	
	
	pDC->MoveTo(m_vertex[0].x, m_vertex[0].y);
	pDC->LineTo(m_vertex[1].x, m_vertex[1].y);
	
}