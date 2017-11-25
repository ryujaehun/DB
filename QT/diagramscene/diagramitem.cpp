#ifndef DEDUG_LOGIC2
#define DEDUG_LOGIC2
#include <QTextStream>
QTextStream out2(stdout);
//  out2 <<__PRETTY_FUNCTION__<< endl;
#endif //DEDUG_LOGIC


#include "diagramitem.h"
#include "arrow.h"

#include <QGraphicsScene>
#include <QGraphicsSceneContextMenuEvent>
#include <QMenu>
#include <QPainter>

//! [0]
DiagramItem::DiagramItem(DiagramType diagramType, QMenu *contextMenu,
             QGraphicsItem *parent)
    : QGraphicsPolygonItem(parent)
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    myDiagramType = diagramType;
    myContextMenu = contextMenu;
    QPolygonF myPolygon1, myPolygon2;                   //두겹 도형 그리기 위한 폴리
    //QRectF qrect(-100, 100 );                                       //외접 사각형
    QPainterPath path;                                  //QPainterPath: 그림 그리는 기능 제공함
    switch (myDiagramType) {
        case StartEnd:                                  //StartEnd: 시작 도형그리는 객체인데 gui에선 구현이 안된듯
            path.moveTo(200, 50);
            path.arcTo(150, 0, 50, 50, 0, 90);
            path.arcTo(50, 0, 50, 50, 90, 90);
            path.arcTo(50, 50, 50, 50, 180, 90);
            path.arcTo(150, 50, 50, 50, 270, 90);
            path.lineTo(200, 25);
            myPolygon = path.toFillPolygon();
            break;
        case Conditional:
            myPolygon << QPointF(-100, 0) << QPointF(0, 100)        //좌표들 설정 내 마우스 좌표 기준으로 팔라미터만큼떨어진 점인듯
                      << QPointF(100, 0) << QPointF(0, -100)
                      << QPointF(-100, 0);
            break;
        case Step:
            myPolygon << QPointF(-100, -100) << QPointF(100, -100)
                      << QPointF(100, 100) << QPointF(-100, 100)
                      << QPointF(-100, -100);
            break;

        case Entity:
            myPolygon << QPointF(-100, -100) << QPointF(100, -100)
                      << QPointF(100, 100) << QPointF(-100, 100)
                      << QPointF(-100, -100);
            break;
        case Weak_entity:
            myPolygon1 << QPointF(-100, -100) << QPointF(100, -100)
                       << QPointF(100, 100) << QPointF(-100, 100)
                       << QPointF(-100, -100);
            myPolygon2 << QPointF(-100+3, -100+3) << QPointF(100-3, -100+3)
                       << QPointF(100-3, 100-3) << QPointF(-100+3, 100-3)
                       << QPointF(-100+3, -100+3);
            myPolygon=myPolygon1+myPolygon2;
            break;
        case Relation:
            myPolygon << QPointF(-100, 0) << QPointF(0, 100)        //좌표들 설정 내 마우스 좌표 기준으로 팔라미터만큼떨어진 점인듯
                      << QPointF(100, 0) << QPointF(0, -100)
                      << QPointF(-100, 0);
            break;
        case Attribute:

        default:
            myPolygon << QPointF(-120, -80) << QPointF(-70, 80)
                      << QPointF(120, 80) << QPointF(70, -80)
                      << QPointF(-120, -80);
            break;
    }
    setPolygon(myPolygon);
    setFlag(QGraphicsItem::ItemIsMovable, true);
    setFlag(QGraphicsItem::ItemIsSelectable, true);
    setFlag(QGraphicsItem::ItemSendsGeometryChanges, true);
}
//! [0]

//! [1]
void DiagramItem::removeArrow(Arrow *arrow)
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    int index = arrows.indexOf(arrow);

    if (index != -1)
        arrows.removeAt(index);
}
//! [1]

//! [2]
void DiagramItem::removeArrows()
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    foreach (Arrow *arrow, arrows) {
        arrow->startItem()->removeArrow(arrow);
        arrow->endItem()->removeArrow(arrow);
        scene()->removeItem(arrow);
        delete arrow;
    }
}
//! [2]

//! [3]
void DiagramItem::addArrow(Arrow *arrow)
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    arrows.append(arrow);
}
//! [3]

//! [4]
QPixmap DiagramItem::image() const
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    QPixmap pixmap(250, 250);
    pixmap.fill(Qt::transparent);
    QPainter painter(&pixmap);
    painter.setPen(QPen(Qt::black, 8));
    painter.translate(125, 125);
    painter.drawPolyline(myPolygon);

    return pixmap;
}
//! [4]

//! [5]
void DiagramItem::contextMenuEvent(QGraphicsSceneContextMenuEvent *event)
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    scene()->clearSelection();
    setSelected(true);
    myContextMenu->exec(event->screenPos());
}
//! [5]

//! [6]
QVariant DiagramItem::itemChange(GraphicsItemChange change, const QVariant &value)
{
  #ifdef DEDUG_LOGIC2
     out2 <<__PRETTY_FUNCTION__<< endl;
  #endif
    if (change == QGraphicsItem::ItemPositionChange) {
        foreach (Arrow *arrow, arrows) {
            arrow->updatePosition();
        }
    }

    return value;
}
//! [6]
