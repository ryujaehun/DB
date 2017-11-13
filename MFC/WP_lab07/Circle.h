#ifndef _CIRCLE_H_
#define _CIRCLE_H_
#include "Shape.h"

class Circle : public Shape{
public:
	Circle();//������-m_shapeType�� CIRCLE�� ������.
	~Circle(){};
	void setLeftTopPoint(int x, int y);//���ʻ�� ����Ʈ�� ��ǥ�� ���Ѵ�.
	void setRightBottomPoint(int x, int y);//������ �ϴ� ����Ʈ�� ��ǥ�� ���Ѵ�.
	void MyEllipse(CDC *parm_dc, int parm_x, int parm_y, int parm_rx, int parm_ry) ;//�� ���� ������ Ÿ���� �׸��� �Լ�
	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags);//�����Լ� Draw: �޹�ư�� ������ �����̸� m_vertex[0]�� m_vertex[1]�� ������.
	virtual void Display(CDC* pDC);//�����Լ� Display: MyEllipse�Լ��� �̿��Ͽ� Ÿ���� �׸�
	virtual void DisplaySelected(CDC* pDC);
	virtual void MoveShape(UINT nChar);

protected:
};

#endif