#include <Windows.h>
#include <iostream>
#include <sql.h>
#include <sqlext.h>
#include<string>
#include<iomanip>
using namespace std;

class SQLERROR
{
private:
	int ERROR_CODE;
public:
	SQLERROR() {}
	void set_error(int num)
	{
		ERROR_CODE = num;
	}
	void print()
	{
		cout <<"ERROR_CODE "<< ERROR_CODE << "!!" << endl;
	}
};

class DataBase
{
private:
	SQLHENV hEnv;
	SQLHDBC hDbc;
	SQLHSTMT hStmt;
	SQLCHAR *ODBC_Name;
	SQLCHAR *ODBC_ID;
	SQLCHAR *ODBC_PW; 
	bool DBExecuteSQL(SQLCHAR* query) //질의 수행
	{
		if (SQLExecDirectA(hStmt, query, SQL_NTS) != SQL_SUCCESS)
			return false;
	return true;
	}
	void DbPrintEmployees()
	{
		SQLINTEGER employee_id;
		SQLCHAR first_name[32];
		SQLCHAR last_name[32];
		SQLCHAR EMAIL[32];
		SQLCHAR PHONE_NUMBER[32];
		SQLTIMESTAMP HIRE_DATE[32];
		SQLCHAR JOB_ID[32];
		SQLINTEGER SALARY;
		SQLREAL COMMISSION_PCT;
		SQLINTEGER MANAGER_ID;
		SQLINTEGER DEPARTMENT_ID;

		SQLLEN i_employee_id, i_first_name, i_last_name, i_EMAIL, i_PHONE_NUMBER, i_HIRE_DATE, i_JOB_ID, i_SALARY, i_COMMISSION_PCT, i_MANAGER_ID, i_DEPARTMENT_ID;

		//select 질의 수행 결과를 저장할 변수를 지정한다.(Column Number는 1부터 시작한다)
		SQLBindCol(hStmt, 1, SQL_INTEGER, &employee_id, sizeof(employee_id), &i_employee_id);
		SQLBindCol(hStmt, 2, SQL_CHAR, first_name, sizeof(first_name), &i_first_name);
		SQLBindCol(hStmt, 3, SQL_CHAR, last_name, sizeof(last_name), &i_last_name);
		SQLBindCol(hStmt, 4, SQL_CHAR, EMAIL, sizeof(EMAIL), &i_EMAIL);
		SQLBindCol(hStmt, 5, SQL_CHAR, PHONE_NUMBER, sizeof(PHONE_NUMBER), &i_PHONE_NUMBER);
		SQLBindCol(hStmt, 6, SQL_TIMESTAMP, HIRE_DATE, sizeof(HIRE_DATE), &i_HIRE_DATE);
		SQLBindCol(hStmt, 7, SQL_CHAR, JOB_ID, sizeof(JOB_ID), &i_JOB_ID);
		SQLBindCol(hStmt, 8, SQL_INTEGER, &SALARY, sizeof(SALARY), &i_SALARY);
		SQLBindCol(hStmt, 9, SQL_REAL, &COMMISSION_PCT, sizeof(COMMISSION_PCT), &i_COMMISSION_PCT);
		SQLBindCol(hStmt, 10, SQL_INTEGER, &MANAGER_ID, sizeof(MANAGER_ID), &i_MANAGER_ID);
		SQLBindCol(hStmt, 11, SQL_INTEGER, &DEPARTMENT_ID, sizeof(DEPARTMENT_ID), &i_DEPARTMENT_ID);


		//질의 수행결과를 hStmt로 가져온다. 이때, 바인딩된 변수에 값이 할당된다.
		while (SQLFetch(hStmt) != SQL_NO_DATA)
		{
			cout.setf(ios::fixed, ios::floatfield);
		
			cout << "EMPLOYEE_ID : " << employee_id << " FIRST_NAME : " << first_name << " LAST_NAME:  " << last_name << " EMAIL: " << EMAIL <<
				" PHONE_NUMBER : " << PHONE_NUMBER << " HIRE_DATE : " << HIRE_DATE << " JOB_ID : " << JOB_ID << " SALARY : " << SALARY << " COMMISSION_PCT : " << COMMISSION_PCT << " MANAGER_ID : " << MANAGER_ID << " DEPARTMENT_ID : " << DEPARTMENT_ID << endl<<endl;
		}

		//hStmt 커서 해제
		if (hStmt)
			SQLCloseCursor(hStmt);
	}
public:
	DataBase():ODBC_Name((SQLCHAR*)"TIBERO"), ODBC_ID((SQLCHAR*)"HR"), ODBC_PW((SQLCHAR*)"tibero")
	{
		SQLERROR ER;
		try {
			if (SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &hEnv) != SQL_SUCCESS) 
				throw ER;
			
			if (SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, SQL_IS_INTEGER) != SQL_SUCCESS)
				throw ER;
			if (SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc) != SQL_SUCCESS)
				throw ER;
			if (SQLConnectA(hDbc, ODBC_Name, SQL_NTS, ODBC_ID, SQL_NTS, ODBC_PW, SQL_NTS) != SQL_SUCCESS)
				throw ER;
			if (SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt) != SQL_SUCCESS)
				throw ER;
		}
		catch (...)
		{
			ER.set_error(1);
			ER.print();
		}
		
	}
	DataBase(SQLCHAR* _ODBC_Name, SQLCHAR* _ODBC_ID, SQLCHAR* _ODBC_PW) :ODBC_Name(_ODBC_Name), ODBC_ID(_ODBC_ID), ODBC_PW(_ODBC_PW) 
	{
	SQLERROR ER;
		try {
			if (SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &hEnv) != SQL_SUCCESS)
				throw ER;
			if (SQLSetEnvAttr(hEnv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, SQL_IS_INTEGER) != SQL_SUCCESS)
				throw ER;
			if (SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &hDbc) != SQL_SUCCESS)
				throw ER;
			if (SQLConnectA(hDbc, ODBC_Name, SQL_NTS, ODBC_ID, SQL_NTS, ODBC_PW, SQL_NTS) != SQL_SUCCESS)
				throw ER;
			if (SQLAllocHandle(SQL_HANDLE_STMT, hDbc, &hStmt) != SQL_SUCCESS)
				throw ER;
		}
		catch (...)
		{
			ER.set_error(2);
			ER.print();
		}
	}
	~DataBase()
	{
		SQLERROR ER;
		try {
			if (hStmt)
				SQLFreeHandle(SQL_HANDLE_STMT, hStmt);
			if (hDbc)
				SQLDisconnect(hDbc);
			if (hDbc)
				SQLFreeHandle(SQL_HANDLE_DBC, hDbc);
			if (hEnv)
				SQLFreeHandle(SQL_HANDLE_ENV, hEnv);
		}
		catch (...)
		{
			ER.set_error(3);
			ER.print();
		}
	
	}
	
	//사원정보조회
	void serchfirst()
	{
		SQLERROR ER;
		string temp;
		cout << "이름을 입력하시오.\n";
		cin >> temp;
		temp = string("select * from employees_cpy where first_name=") + '\'' + temp + '\'';
		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp.c_str()) == false)
				throw ER;
			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(3);
			ER.print();
		}
	}
	void serchlast()
	{
		SQLERROR ER;
		string temp;
		cout << "이름을 입력하시오.\n";
		cin >> temp;
		temp = string("select * from employees_cpy where last_name=") + '\'' + temp + '\'';
		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp.c_str()) == false)
				throw ER;
			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(4);
			ER.print();
		}
	}
	void serchspecial()
	{
		SQLERROR ER;
		string temp;
		cout << "특정 문자를 입력하시오.\n";
		cin >> temp;
		temp = string("select * from employees_cpy where first_name like ") + "\'%" + temp + "%\'" + " or  last_name like " + "\'%" + temp + "%\'";
		
		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp.c_str()) == false)
				throw ER;
			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(5);
			ER.print();
		}
	}
	void serchdepartment()
	{
		SQLERROR ER;
		string temp;
		cout << "부서번호를 입력하시오.\n";
		cin >> temp;
		temp = string("select * from employees_cpy where DEPARTMENT_ID=") +  temp  ;
		
		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp.c_str()) == false)
				throw ER;
			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(6);
			ER.print();
		}
	}
	void serchminmax()
	{
		SQLERROR ER;
		string temp;
		cout << "부서번호를 입력하시오.\n";
		cin >> temp;
		temp = string("select * from (select * from employees_cpy where DEPARTMENT_ID=") + temp +string(" order by salary) where rownum<4 or rownum>31");

		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp.c_str()) == false)
				throw ER;
			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(7);
			ER.print();
		}
	}

	// 사원점보수정
	void setSalary()
	{
		SQLERROR ER;
		string temp1,temp2,temp3;
		cout << "사원번호를 입력하시오.\n";
		cin >> temp1;
		cout << "급여를 입력하시오.\n";
		cin >> temp2;
		temp3 = string("select * from employees_cpy where EMPLOYEE_ID = ") + temp1;
		temp1 = string("update employees_cpy set salary =") + temp2 + string(" where EMPLOYEE_ID = ")+temp1;
		
		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp1.c_str()) == false)
				throw ER;
			if (DBExecuteSQL((SQLCHAR*)temp3.c_str()) == false)
				throw ER;

			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(8);
			ER.print();
		}
	}
	void setNumber()
	{
		SQLERROR ER;
		string temp1, temp2, temp3;
		cout << "사원번호를 입력하시오.\n";
		cin >> temp1;
		cout << "전화번호를 입력하시오.\n";
		cin >> temp2;
		temp3 = string("select * from employees_cpy where EMPLOYEE_ID = ") + temp1;
		temp1 = string("update employees_cpy set PHONE_NUMBER =") +'\''+ temp2 + '\'' + string(" where EMPLOYEE_ID = ") + temp1;

		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp1.c_str()) == false)
				throw ER;
			if (DBExecuteSQL((SQLCHAR*)temp3.c_str()) == false)
				throw ER;

			DbPrintEmployees();
		}
		catch (...)
		{
			ER.set_error(9);
			ER.print();
		}
	}
	
	void setDepartment()
	{
		SQLERROR ER;
		string temp1, temp2, temp3;
		cout << "사원번호를 입력하시오.\n";
		cin >> temp1;
		cout << "부서번호를 입력하시오.\n";
		cin >> temp2;
		temp3 = string("select * from employees_cpy where EMPLOYEE_ID = ") + temp1;
		temp1 = string("update employees_cpy set DEPARTMENT_ID =") + temp2 + string(" where EMPLOYEE_ID = ") + temp1;

		
		try {
			if (DBExecuteSQL((SQLCHAR*)temp1.c_str()) == false)
				throw ER;
			if (DBExecuteSQL((SQLCHAR*)temp3.c_str()) == false)
				throw ER;

			DbPrintEmployees();
		}

		
		catch (...)
		{
			ER.set_error(10);
			ER.print();
		}
	}

};


int main()
{

	DataBase DB;
	while (true)
	{
		int choice;
		
		int num;
		cout << "\n사원조회시스템" << endl;
		cout << "1. 조회 2.변경 3. 종료" << endl;
		cin >> choice;
		system("cls");
		if (choice == 1)
		{
			cout << "1. first_name으로 조회 2.last_name으로 조회 3. 이름에 포함된 문자로 조회 4. 부서이름으로 조회 5. 특정부서의 top3 min,max  salary" << endl;
			cin >>num;
			system("cls");
			switch (num)
			{
			case 1:DB.serchfirst(); break;
			case 2:DB.serchlast(); break;
			case 3:DB.serchspecial(); break;
			case 4:DB.serchdepartment(); break;
			case 5:DB.serchminmax(); break;
			default:break;
			}
		}

		else if (choice == 2)
		{
			cout << "1. salary 변경 2. 부서 이동 3. 핸드폰번호 수정" << endl;
			cin >> num;
			system("cls");
			switch (num)
			{
			case 1:DB.setSalary(); break;
			case 2:DB.setDepartment(); break;
			case 3:DB.setNumber(); break;
			default:break;
			}

		}
		else if (choice == 3)
		{
			break;
		}
		else
		{
			break;
		}
	
	}
	
	return 0;
}

