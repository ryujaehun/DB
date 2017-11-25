#ifndef DATABASE_H
#define DATABASE_H

#include <QtSql>
//! [0]
class Database : public QSqlDatabase
{

public:
    void connectDb();
    void createModel();
    void viewData();

};
//! [0]
//!
#endif // DATABASE_H
