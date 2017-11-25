

#include <QApplication>
#include "mainwindow.h"

#ifndef DEDUG_LOGIC3
#define DEDUG_LOGIC3
#include <QTextStream>
QTextStream out3(stdout);
//  out <<__PRETTY_FUNCTION__<< endl;
#endif //DEDUG_LOGIC3

int main(int argc, char **argv)
{
  #ifdef DEDUG_LOGIC3
      out3 <<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC3
    Q_INIT_RESOURCE(undo);

    QApplication app(argc, argv);

    MainWindow win;
    win.resize(800, 600);
    win.show();

    return app.exec();
};
