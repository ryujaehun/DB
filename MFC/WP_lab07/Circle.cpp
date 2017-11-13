#include "stdafx.h"
#include "Circle.h"
#include <math.h>

Circle::Circle(){
	m_shapeType=CIRCLE;
}

void Circle::Draw(MOUSEEVENT ev, int x, int y, UINT nFlags){
	
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

void Circle::setLeftTopPoint(int x, int y){
	m_vertex[0]=CPoint(x,y);
}

void Circle::setRightBottomPoint(int x, int y){
	m_vertex[1]= CPoint(x,y);
}

void Circle::DisplaySelected(CDC* pDC){
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

void Circle::MoveShape(UINT nChar){
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


void Circle::Display(CDC* pDC){
	CPen pen;
	pen.CreatePen(PS_DOT, width, RGB(0, 0, 0));
	CPen* old = pDC->SelectObject(&pen);
	if( m_flags & MK_SHIFT ) {
		m_vertex[1].y = m_vertex[0].y + (m_vertex[1].x - m_vertex[0].x);
	}
	int mid_x, mid_y, rx, ry;
	mid_x=(m_vertex[1].x+m_vertex[0].x)/2;
	mid_y=(m_vertex[1].y+m_vertex[0].y)/2;
	rx=abs((m_vertex[1].x-m_vertex[0].x)/2);
	ry=abs((m_vertex[1].y-m_vertex[0].y)/2);
	MyEllipse(pDC, mid_x, mid_y, rx, ry);
}

void Circle::MyEllipse(CDC *parm_dc, int parm_x, int parm_y, int parm_rx, int parm_ry) {
			parm_dc->MoveTo(parm_x+parm_rx, parm_y);
	for(int i = 0; i < 360; i++){ 

        parm_dc->LineTo( 
		(parm_x + int((cos(i*3.141592/180)*parm_rx))), 
        (parm_y + int(sin(i*3.141592/180)*parm_ry))
        );
	}
}

