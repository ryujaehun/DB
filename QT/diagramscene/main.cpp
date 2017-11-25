#ifndef DEDUG_LOGIC5
#define DEDUG_LOGIC5
#include <QTextStream>
QTextStream out5(stdout);
//
#endif //DEDUG_LOGIC


#include "mainwindow.h"

#include <QApplication>

int main(int argv, char *args[])
{
  #ifdef DEDUG_LOGIC5
  out5<<__PRETTY_FUNCTION__<< endl;
  #endif //DEDUG_LOGIC
    Q_INIT_RESOURCE(diagramscene);

    QApplication app(argv, args);
    MainWindow mainWindow;
    mainWindow.setGeometry(100, 100, 800, 500);
    mainWindow.show();

    return app.exec();
}
