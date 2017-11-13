#ifndef _QUAD_H_
#define _QUAD_H_
#include "Shape.h"

class Quad : public Shape{
public:
	Quad();//생성자-m_shapeType을 QUAD로 설정함.
	~Quad(){};
	void setLeftTopPoint(int x, int y);//왼쪽상단 포인트의 좌표를 정한다.
	void setRightBottomPoint(int x, int y);//오른쪽 하단 포인트의 좌표를 정한다.

	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags);//가상함수 Draw: 왼버튼이 눌리고 움직이면 m_vertex[0]과 m_vertex[1]이 설정됨.
	virtual void Display(CDC* pDC);//가상함수 Display: 4점을 선으로 이어 직사각형을 그림
	virtual void DisplaySelected(CDC* pDC);
	virtual void MoveShape(UINT nChar);
	
protected:
};

#endif