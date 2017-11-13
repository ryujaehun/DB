
#include "stdafx.h"
#include "Shape.h"


IMPLEMENT_DYNAMIC(Shape, CObject)
void Shape::Serialize(CArchive& ar){
	CObject::Serialize(ar);
	if(ar.IsStoring())
		ar<<shapeType<<m_start<<m_vertex[1]<<m_vertex[2]<<m_flags;
	else
		ar>>shapeType>>m_start>>m_vertex[1]>>m_vertex[2]>>m_flags;
}

void Shape::SaveToFile(CFile& fout)
{
	CArchive ar(&fout, CArchive::store);
	Serialize(ar);
}

void Shape::LoadFromFile(CFile &fin)
{
	CArchive ar(&fin, CArchive::store);
	try{
		Serialize(ar);
	}catch(CFileException *fe){
		AfxMessageBox("CFileException");
		fe->ReportError();
	}catch(CArchiveException *fe){
		AfxMessageBox("CArchiveException");
		fe->ReportError();
	}
	catch(CMemoryException *fe){
		AfxMessageBox("CMemoryException");
		fe->ReportError();
	}
	ar.Close();
	fin.Close();

}

void Shape::incWidth(){
	width++;
}

void Shape::decWidth() {
	width--;
}