#ifndef DEDUG_LOGIC4
#define DEDUG_LOGIC4
#include <QTextStream>
QTextStream out4(stdout);
//
#endif //DEDUG_LOGIC


#include "diagramtextitem.h"
#include "diagramscene.h"



//! [0]
DiagramTextItem::DiagramTextItem(QGraphicsItem *parent)
    : QGraphicsTextItem(parent)
{
  #ifdef DEDUG_LOGIC4
  out4 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    setFlag(QGraphicsItem::ItemIsMovable);
    setFlag(QGraphicsItem::ItemIsSelectable);
}

QVariant DiagramTextItem::itemChange(GraphicsItemChange change,
                     const QVariant &value)
{
  #ifdef DEDUG_LOGIC4
  out4 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    if (change == QGraphicsItem::ItemSelectedHasChanged)
        emit selectedChange(this);
    return value;
}

void DiagramTextItem::focusOutEvent(QFocusEvent *event)
{
  #ifdef DEDUG_LOGIC4
  out4 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    setTextInteractionFlags(Qt::NoTextInteraction);
    emit lostFocus(this);
    QGraphicsTextItem::focusOutEvent(event);
}

void DiagramTextItem::mouseDoubleClickEvent(QGraphicsSceneMouseEvent *event)
{
  #ifdef DEDUG_LOGIC4
  out4 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    if (textInteractionFlags() == Qt::NoTextInteraction)
        setTextInteractionFlags(Qt::TextEditorInteraction);
    QGraphicsTextItem::mouseDoubleClickEvent(event);
}
