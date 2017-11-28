#include "database.h"

#include <QTableView>

//! [0]
void Database::connectDb()
{



    QSqlDatabase db = QSqlDatabase::addDatabase("QOCI");

    QStringList driverList;
    driverList = QSqlDatabase::drivers();


        db.setHostName("test");
        db.setDatabaseName("TIBERO");
        db.setUserName("HR");
        db.setPassword("tibero");



    if(db.open())
    {
        qDebug() << "connected " << db.hostName();

    }else{

        messageBox;
        messageBox.critical(0,"DB connect Error","An connect error has occured !");


        qDebug()<<"Error while opening DB: " << db.databaseName()
               << db.lastError();
    }
}
