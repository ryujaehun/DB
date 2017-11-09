#include <Windows.h>
#include <iostream>
#include <sql.h>
#include <sqlext.h>
using namespace std;


//ODBC conn
SQLHENV hEnv;
SQLHDBC hDbc;
SQLHSTMT hStmt;

//���� ����
SQLCHAR *ODBC_Name = (SQLCHAR*)"TIBERO";
SQLCHAR *ODBC_ID = (SQLCHAR*)"HR";
SQLCHAR *ODBC_PW = (SQLCHAR*)"tibero";

bool DBConnect();
void DBDisConnect();
bool DBExecuteSQL();
bool DBExecuteSQL(SQLCHAR* query);
bool DBPrepare(SQLCHAR* query_for_statement);


void PrintResult()//Select������ ������ ������ �����ϴ� ���
{
    SQLINTEGER employee_id;
    SQLCHAR first_name[32];
    SQLCHAR last_name[32];

    SQLLEN i_employee_id, i_first_name, i_last_name;

    //select ���� ���� ����� ������ ������ �����Ѵ�.(Column Number�� 1���� �����Ѵ�)
    SQLBindCol(hStmt, 1, SQL_INTEGER, &employee_id, sizeof(employee_id), &i_employee_id);
    SQLBindCol(hStmt, 2, SQL_CHAR, first_name, sizeof(first_name), &i_first_name);
    SQLBindCol(hStmt, 3, SQL_CHAR, last_name, sizeof(last_name), &i_last_name);

    //���� �������� hStmt�� �����´�. �̶�, ���ε��� ������ ���� �Ҵ�ȴ�.
    while (SQLFetch(hStmt) != SQL_NO_DATA)
    {
        cout << employee_id << ' ' << first_name << ' ' << last_name << endl;
    }

    //hStmt Ŀ�� ����
    if (hStmt)
        SQLCloseCursor(hStmt);
   
}
bool InsertJob(SQLCHAR* job_id, SQLCHAR* job_name, SQLINTEGER min_salary, SQLINTEGER max_salary)
{
    SQLBindParameter(hStmt, 1, SQL_PARAM_INPUT, SQL_CHAR, SQL_CHAR, 0, 0, job_id, 0, NULL);
    SQLBindParameter(hStmt, 2, SQL_PARAM_INPUT, SQL_CHAR, SQL_CHAR, 0, 0, job_name, 0, NULL);
    SQLBindParameter(hStmt, 3, SQL_PARAM_INPUT, SQL_INTEGER, SQL_INTEGER, 0, 0, &min_salary, 0, NULL);
    SQLBindParameter(hStmt, 4, SQL_PARAM_INPUT, SQL_INTEGER, SQL_INTEGER, 0, 0, &max_salary, 0, NULL);

    if (DBExecuteSQL())
        return true;
    return false;
}

void InsertTest() //Insert���� �׽�Ʈ
{
    DBPrepare((SQLCHAR*)"insert into jobs_copy values(?,?,?,?)");

    InsertJob((SQLCHAR*)"CP_EN", (SQLCHAR*)"Computer Engineer", 20000, 60000);
    InsertJob((SQLCHAR*)"AT_PT", (SQLCHAR*)"Artist Painter", 10000, 80000);
}


int main()
{
    if (DBConnect() == false)
    {
        cout << "���� ����" << endl;
        return -1;
    }

    //�׽�Ʈ �ڵ� ���� ��ġ
    

    DBDisConnect();
    return 0;
}

bool DBConnect() //�����ͺ��̽� ����
{
    if (SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &hEnv) != SQL_SUCCESS)
        return false;
    if (SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, SQL_IS_INTEGER) != SQL_SUCCESS)
        return false;
    if (SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc) != SQL_SUCCESS)
        return false;
    if (SQLConnectA(hDbc, ODBC_Name, SQL_NTS, ODBC_ID, SQL_NTS, ODBC_PW, SQL_NTS) != SQL_SUCCESS)
        return false;
    if (SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt) != SQL_SUCCESS)
        return false;
    return true;
}
void DBDisConnect() //�����ͺ��̽� ���� ����
{
    if (hStmt)
        SQLFreeHandle(SQL_HANDLE_STMT, hStmt);
    if (hDbc)
        SQLDisconnect(hDbc);
    if (hDbc)
        SQLFreeHandle(SQL_HANDLE_DBC, hDbc);
    if (hEnv)
        SQLFreeHandle(SQL_HANDLE_ENV, hEnv);
}
bool DBExecuteSQL()
{
    if (SQLExecute(hStmt) != SQL_SUCCESS)
        return false;
    return true;
}
bool DBPrepare(SQLCHAR* query_for_statement)
{
    if (SQLPrepareA(hStmt, query_for_statement, SQL_NTS) != SQL_SUCCESS)
        return false;
    return true;
}

bool DBExecuteSQL(SQLCHAR* query) //���� ����
{
    if (SQLExecDirectA(hStmt, query, SQL_NTS) != SQL_SUCCESS)
        return false;
    return true;
}