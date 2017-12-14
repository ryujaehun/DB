
/**
  * EReditor
  * ERObject.java
  * Created by Palle on 30.05.2014
  * Copyright (c) 2014 - 2017 Palle.
  *
  * Permission is hereby granted, free of charge, to any person obtaining a copy
  * of this software and associated documentation files (the "Software"), to deal
  * in the Software without restriction, including without limitation the rights
  * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  * copies of the Software, and to permit persons to whom the Software is
  * furnished to do so, subject to the following conditions:
  *
  * The above copyright notice and this permission notice shall be included in
  * all copies or substantial portions of the Software.
  *
  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
  * THE SOFTWARE.
  */

import java.awt.Graphics2D;
import java.awt.Point;
import java.awt.Rectangle;
import java.io.Serializable;

public abstract class ERObject implements Serializable
{
	private static final long serialVersionUID = 1L;
	
	protected Rectangle	bounds		= new Rectangle();				//사각형 바운더리
	protected boolean	selected	= false;						//선택 여부
	protected String	name		= "Empty";						//오브젝트에 사용될 이름
	protected boolean	weak;										//약한 개체 속성
	//선택 해제
	public void deselect()
	{
		selected = false;
	}
	//객체 위치 얻음(바운더리 사각형의 x, y 포인트)
	public Point getLocation()
	{
		return bounds.getLocation();
	}
	//이름 반환
	public String getName()
	{
		return name;
	}
	//선택사각형에 영향을 받는지?(드래그로 도형 선택할 때 사용하는듯)
	public boolean isAffectedBySelectionRectangle(Rectangle r)
	{
		return bounds.intersects(r);
	}
	public Rectangle getBounds() {
		return bounds;
	}
	public void setBounds(Rectangle bounds) {
		this.bounds = bounds;
	}
	//객체의 바운더리 사각형 내에 포인터가 있으면 참
	public boolean isAffectedByTouch(Point p)
	{
		return bounds.contains(p);
	}
	//선택 여부 반환
	public boolean isSelected()
	{
		return selected;
	}
	//약한 객체 여부 반환
	public boolean isWeak()
	{
		return weak;
	}
	//도형 타입에 따라 그림그리는 포인터
	public abstract void paint(Graphics2D g);
	//선택하면 true
	public void select()
	{
		selected = true;
	}
	//객체 위치 설정(x, y)
	public void setLocation(int x, int y)
	{
		bounds.x = x;
		bounds.y = y;
	}
	//객체 위치 설정(Point)
	public void setLocation(Point p)
	{
		bounds.x = p.x;
		bounds.y = p.y;
	}
	//이름 설정
	public void setName(String name)
	{
		if (name.length() == 0)
			name = " ";
		this.name = name;
	}
	//강약 설정
	public void setWeak(boolean aFlag)
	{
		weak = aFlag;
	}
}
