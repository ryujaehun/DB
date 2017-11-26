/****************************************************************************
**
** Copyright (C) 2016 The Qt Company Ltd.
** Contact: https://www.qt.io/licensing/
**
** This file is part of the examples of the Qt Toolkit.
**
** $QT_BEGIN_LICENSE:BSD$
** Commercial License Usage
** Licensees holding valid commercial Qt licenses may use this file in
** accordance with the commercial license agreement provided with the
** Software or, alternatively, in accordance with the terms contained in
** a written agreement between you and The Qt Company. For licensing terms
** and conditions see https://www.qt.io/terms-conditions. For further
** information use the contact form at https://www.qt.io/contact-us.
**
** BSD License Usage
** Alternatively, you may use this file under the terms of the BSD license
** as follows:
**
** "Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are
** met:
**   * Redistributions of source code must retain the above copyright
**     notice, this list of conditions and the following disclaimer.
**   * Redistributions in binary form must reproduce the above copyright
**     notice, this list of conditions and the following disclaimer in
**     the documentation and/or other materials provided with the
**     distribution.
**   * Neither the name of The Qt Company Ltd nor the names of its
**     contributors may be used to endorse or promote products derived
**     from this software without specific prior written permission.
**
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
** "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
** LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
** A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
** OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
** SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
** LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
** DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
** THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
** OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE."
**
** $QT_END_LICENSE$
**
****************************************************************************/

#include "diagramitem.h"
#include "arrow.h"
#include "location.h"
#include <QGraphicsScene>
#include <QGraphicsSceneContextMenuEvent>
#include <QGraphicsEllipseItem>
#include <QMenu>
#include <QPainter>

//! [0]
DiagramItem::DiagramItem(DiagramType diagramType, QMenu *contextMenu,
             QGraphicsItem *parent)
    : QGraphicsPolygonItem(parent)
{
    myDiagramType = diagramType;
    myContextMenu = contextMenu;
    /**/
    Location dl = Location();
    const QPoint lt = QPoint(dl.left_top_x, dl.left_top_y);
    const QPoint rb = QPoint(dl.right_bottom_x, dl.right_bottom_y);
    QPolygonF myPolygon1, myPolygon2;
    /**/
    QPainterPath path;
    QPainter painter;
    switch (myDiagramType) {
//        case StartEnd:
//            path.moveTo(200, 50);
//            path.arcTo(150, 0, 50, 50, 0, 90);
//            path.arcTo(50, 0, 50, 50, 90, 90);
//            path.arcTo(50, 50, 50, 50, 180, 90);
//            path.arcTo(150, 50, 50, 50, 270, 90);
//            path.lineTo(200, 25);
//            myPolygon = path.toFillPolygon();
//            break;
//        case Conditional:
//            myPolygon << QPointF(-100, 0) << QPointF(0, 100)
//                      << QPointF(100, 0) << QPointF(0, -100)
//                      << QPointF(-100, 0);
//            break;
//        case Step:
//            myPolygon << QPointF(-100, -100) << QPointF(100, -100)
//                      << QPointF(100, 100) << QPointF(-100, 100)
//                      << QPointF(-100, -100);
//            break;

        case Entity:
            myPolygon= QPolygon(QRect(lt, rb), true);
            break;
        case Weak_entity:
            myPolygon1 = QPolygon(QRect(lt, rb), true);
            myPolygon2 = QPolygon(QRect(lt+QPoint(3,3), rb-QPoint(3,3)), true);
            myPolygon=myPolygon1+myPolygon2;
            break;
        case Relationship:
            myPolygon << QPointF((lt.x()+rb.x())/2, lt.y()) << QPointF(rb.x(), (lt.y()+rb.y())/2)
                      << QPointF((lt.x()+rb.x())/2, rb.y()) << QPointF(lt.x(), (lt.y()+rb.y())/2)
                      << QPointF((lt.x()+rb.x())/2, lt.y());
            break;
        case Weak_relationship:
            myPolygon1 << QPointF((lt.x()+rb.x())/2, lt.y()) << QPointF(rb.x(), (lt.y()+rb.y())/2)
                       << QPointF((lt.x()+rb.x())/2, rb.y()) << QPointF(lt.x(), (lt.y()+rb.y())/2)
                       << QPointF((lt.x()+rb.x())/2, lt.y());

            myPolygon2 << QPointF((lt.x()+rb.x())/2, lt.y()+5) << QPointF(rb.x()-10, (lt.y()+rb.y())/2)
                       << QPointF((lt.x()+rb.x())/2, rb.y()-5) << QPointF(lt.x()+10, (lt.y()+rb.y())/2)
                       << QPointF((lt.x()+rb.x())/2, lt.y()+5);
            myPolygon=myPolygon1;
            myPolygon=myPolygon1+myPolygon2;
            break;
        case Multi_Attribute:
            path.moveTo(QPointF(rb.x(), (lt.y()+rb.y())/2));
            path.arcTo(QRect(lt, rb), 0, 360);
            path.moveTo(QPointF(rb.x()-3, (lt.y()+rb.y())/2));
            path.arcTo(QRect(lt+QPoint(3,3), rb-QPoint(3,3)), 0, 360);
            myPolygon=path.toFillPolygon();
        case Attribute:
            //myPolygon = QPolygon(QGraphicsEllipseItem(QRect(QRect(lt, rb)),0));
            path.moveTo(QPointF(rb.x(), (lt.y()+rb.y())/2));
            path.arcTo(QRect(lt, rb), 0, 360);
            myPolygon = path.toFillPolygon();
            break;
        case Relation:
            myPolygon1 = QPolygon(QRect(lt, rb), true);
            path.addPolygon(myPolygon1);
            painter.fillPath((path), QBrush(QColor("blue")));
            myPolygon2 = QPolygon(QRect(QPoint(lt.x(), lt.y()+30), rb), true);
            myPolygon=myPolygon1;
            break;

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
    int index = arrows.indexOf(arrow);

    if (index != -1)
        arrows.removeAt(index);
}
//! [1]

//! [2]
void DiagramItem::removeArrows()
{
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
    arrows.append(arrow);
}
//! [3]

//! [4]
QPixmap DiagramItem::image() const
{
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
    scene()->clearSelection();
    setSelected(true);
    myContextMenu->exec(event->screenPos());
}
//! [5]

//! [6]
QVariant DiagramItem::itemChange(GraphicsItemChange change, const QVariant &value)
{
    if (change == QGraphicsItem::ItemPositionChange) {
        foreach (Arrow *arrow, arrows) {
            arrow->updatePosition();
        }
    }

    return value;
}
//! [6]
