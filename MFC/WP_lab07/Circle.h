#ifndef _CIRCLE_H_
#define _CIRCLE_H_
#include "Shape.h"

class Circle : public Shape{
public:
	Circle();//생성자-m_shapeType을 CIRCLE로 설정함.
	~Circle(){};
	void setLeftTopPoint(int x, int y);//왼쪽상단 포인트의 좌표를 정한다.
	void setRightBottomPoint(int x, int y);//오른쪽 하단 포인트의 좌표를 정한다.
	void MyEllipse(CDC *parm_dc, int parm_x, int parm_y, int parm_rx, int parm_ry) ;//두 점을 가지고 타원을 그리는 함수
	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags);//가상함수 Draw: 왼버튼이 눌리고 움직이면 m_vertex[0]과 m_vertex[1]이 설정됨.
	virtual void Display(CDC* pDC);//가상함수 Display: MyEllipse함수를 이용하여 타원을 그림
	virtual void DisplaySelected(CDC* pDC);
	virtual void MoveShape(UINT nChar);

protected:
};

#endif