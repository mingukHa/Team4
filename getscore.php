<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "db_score";


$conn = new mysqli($servername, $username, $password, $dbname);

if($conn->connect_error)
{
	die("connection failed: ".$conn->connect_error);
}

$sql = "SELECT * FROM tb_score";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	echo "[";
	while($row = $result->fetch_assoc()) {
		echo "{'id': '".$row['id'].
			 "', 'score': '".$row['score']."'},";
	}
	echo "]";
}

$conn->close();
?>