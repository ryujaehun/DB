#include "database.h"

#include <QTableView>

//! [0]
void Database::connectDb()
{



    QSqlDatabase db = QSqlDatabase::addDatabase("QOCI");

    QStringList driverList;
    driverList = QSqlDatabase::drivers();


        db.setHostName("tibero");
        db.setDatabaseName("XE");
        db.setUserName("tibero");
        db.setPassword("tibero");



    if(db.open())
    {
        qDebug() << "connected " << db.hostName();

    }else{
        qDebug()<<"Error while opening DB: " << db.databaseName()
               << db.lastError();
    }
}
