#ifndef _LINE_H_
#define _LINE_H_
#include "Shape.h"

class Line : public Shape{
public:
	Line ();//������ m_shapeType�� LINE���� �����.

	~Line (){};//�Ҹ���
	void setLeftTopPoint(int x, int y);//���ʻ�� ����Ʈ�� ��ǥ�� ���Ѵ�.
	void setRightBottomPoint(int x, int y);//������ �ϴ� ����Ʈ�� ��ǥ�� ���Ѵ�.


	virtual void Draw(MOUSEEVENT ev, int x, int y, UINT nFlags);//�����Լ� Draw: �޹�ư�� ������ �����̸� m_vertex[0]�� m_vertex[1]�� ������.
	virtual void Display(CDC* pDC);//�����Լ� Display: ������Ʈ�� ������ �̾� �����׸�
	virtual void DisplaySelected(CDC* pDC);
	virtual void MoveShape(UINT nChar);
protected:
};

#endif