#ifndef DATABASE_H
#define DATABASE_H
#include <QMessageBox>
#include <QtSql>
//! [0]
class Database : public QSqlDatabase
{

public:
       QMessageBox messageBox;
    void connectDb();
    void createModel();
    void viewData();

};
//! [0]
//!
#endif // DATABASE_H
