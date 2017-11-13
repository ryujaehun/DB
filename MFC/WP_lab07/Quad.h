#ifndef _QUAD_H_
#define _QUAD_H_
#include "Shape.h"

class Quad : public Shape{
public:
	Quad();//������-m_shapeType�� QUAD�� ������.
	~Quad(){};
	void setLeftTopPoint(int x, int y);//���ʻ�� ����Ʈ�� ��ǥ�� ���Ѵ�.
	void setRightBottomPoint(int x, int y);//������ �ϴ� ����Ʈ�� ��ǥ�� ���Ѵ�.

	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags);//�����Լ� Draw: �޹�ư�� ������ �����̸� m_vertex[0]�� m_vertex[1]�� ������.
	virtual void Display(CDC* pDC);//�����Լ� Display: 4���� ������ �̾� ���簢���� �׸�
	virtual void DisplaySelected(CDC* pDC);
	virtual void MoveShape(UINT nChar);
	
protected:
};

#endif