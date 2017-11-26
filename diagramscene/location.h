#ifndef LOCATION_H
#define LOCATION_H
#include <QGraphicsScene>
//by seok won
struct Location{
    int left_top_x;
    int left_top_y;
    int right_bottom_x;
    int right_bottom_y;

    Location() {
        left_top_x=-100;
        left_top_y=-50;
        right_bottom_x=100;
        right_bottom_y=50;
    }

    Location(qreal ltx, qreal lty, qreal rbx, qreal rby) {
        left_top_x=-ltx;
        left_top_y=-lty;
        right_bottom_x=rbx;
        right_bottom_y=rby;
    }

    void setLeftTop(qreal ltx, qreal lty){      //set left top location (ltx, lty)
        left_top_x=ltx;
        left_top_y=lty;
    }

    void setRightBottom(qreal rbx, qreal rby){  //set right bottom location (rbx, rby)
        right_bottom_x=rbx;
        right_bottom_y=rby;
    }
};

#endif // LOCATION_H
