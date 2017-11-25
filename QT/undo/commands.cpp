
#ifndef DEDUG_LOGIC1
#define DEDUG_LOGIC1
#include <QTextStream>
QTextStream out1(stdout);
#endif //DEDUG_LOGIC

#include "commands.h"

static const int setShapeRectCommandId = 1;
static const int setShapeColorCommandId = 2;

/******************************************************************************
** AddShapeCommand
*/

AddShapeCommand::AddShapeCommand(Document *doc, const Shape &shape, QUndoCommand *parent)
    : QUndoCommand(parent)
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc = doc;
    m_shape = shape;
}

void AddShapeCommand::undo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc->deleteShape(m_shapeName);
}

void AddShapeCommand::redo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_shapeName = m_doc->addShape(m_shape);
    setText(QObject::tr("Add %1").arg(m_shapeName));
}


RemoveShapeCommand::RemoveShapeCommand(Document *doc, const QString &shapeName,
                                        QUndoCommand *parent)
    : QUndoCommand(parent)
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    setText(QObject::tr("Remove %1").arg(shapeName));
    m_doc = doc;
    m_shape = doc->shape(shapeName);
    m_shapeName = shapeName;
}

void RemoveShapeCommand::undo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_shapeName = m_doc->addShape(m_shape);
}

void RemoveShapeCommand::redo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc->deleteShape(m_shapeName);
}



SetShapeColorCommand::SetShapeColorCommand(Document *doc, const QString &shapeName,
                                            const QColor &color, QUndoCommand *parent)
    : QUndoCommand(parent)
{

  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    setText(QObject::tr("Set %1's color").arg(shapeName));

    m_doc = doc;
    m_shapeName = shapeName;
    m_oldColor = doc->shape(shapeName).color();
    m_newColor = color;
}

void SetShapeColorCommand::undo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc->setShapeColor(m_shapeName, m_oldColor);
}

void SetShapeColorCommand::redo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc->setShapeColor(m_shapeName, m_newColor);
}

bool SetShapeColorCommand::mergeWith(const QUndoCommand *command)
{

  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    if (command->id() != setShapeColorCommandId)
        return false;

    const SetShapeColorCommand *other = static_cast<const SetShapeColorCommand*>(command);
    if (m_shapeName != other->m_shapeName)
        return false;

    m_newColor = other->m_newColor;
    return true;
}

int SetShapeColorCommand::id() const
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    return setShapeColorCommandId;
}

/******************************************************************************
** SetShapeRectCommand
*/

SetShapeRectCommand::SetShapeRectCommand(Document *doc, const QString &shapeName,
                                            const QRect &rect, QUndoCommand *parent)
    : QUndoCommand(parent)
{

  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC

    setText(QObject::tr("Change %1's geometry").arg(shapeName));

    m_doc = doc;
    m_shapeName = shapeName;
    m_oldRect = doc->shape(shapeName).rect();
    m_newRect = rect;
}

void SetShapeRectCommand::undo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC


    m_doc->setShapeRect(m_shapeName, m_oldRect);
}

void SetShapeRectCommand::redo()
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    m_doc->setShapeRect(m_shapeName, m_newRect);
}

bool SetShapeRectCommand::mergeWith(const QUndoCommand *command)
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC

    if (command->id() != setShapeRectCommandId)
        return false;

    const SetShapeRectCommand *other = static_cast<const SetShapeRectCommand*>(command);
    if (m_shapeName != other->m_shapeName)
        return false;

    m_newRect = other->m_newRect;
    return true;
}

int SetShapeRectCommand::id() const
{
  #ifdef DEDUG_LOGIC1
    out1 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    return setShapeRectCommandId;
}
