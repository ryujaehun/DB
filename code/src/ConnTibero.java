import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;

public class ConnTibero {

	Connection conn = null;
	Statement stmt = null;
	ResultSet rs = null;
	String DB_DRV = "com.tmax.tibero.jdbc.TbDriver";
	String DB_IP = "localhost";
	String DB_PORT = "8629";
	String DB_SID = "tibero";
	String DB_ID = "ER";
	String DB_PWD = "tibero";
	String DB_URL = "jdbc:tibero:thin:@"+ DB_IP + ":" + DB_PORT + ":" + DB_SID;
	


	public void connect() {
		System.out.println("=================================");
		System.out.println("DB_DRV : " + DB_DRV);
		System.out.println("DB_URL : " + DB_URL);
		System.out.println("DB_ID : " + DB_ID);
		System.out.println("DB_PWD : " + DB_PWD);
		try {
			Class.forName(DB_DRV);
			conn = DriverManager.getConnection(DB_URL, DB_ID, DB_PWD);
			System.out.println("Tibero connect Success");
			System.out.println("=================================");
			System.out.println();
		} catch (Exception ex) {
			// TODO Auto-generated catch block
			ex.printStackTrace();
		}

	}
	
	public ResultSet excute(String str) {
		try {
			stmt = conn.createStatement();
			rs = stmt.executeQuery(str);
			System.out.println("=================================");
			System.out.println("SQL : " + str);
			System.out.println("=================================");
			return rs;
			
		} catch (Exception ex) {
			// TODO Auto-generated catch block
			ex.printStackTrace();
		}
		
		return null;
	}
}