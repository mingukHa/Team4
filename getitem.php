<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "test1";


$conn = new mysqli($servername, $username, $password, $dbname);

if($conn->connect_error)
{
	die("connection failed: ".$conn->connect_error);
}

$sql = "SELECT * FROM `item`";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	echo "[";
	while($row = $result->fetch_assoc()) {
		echo "{'item_num': '".$row['item_num'].
			 "', 'item_name': '".$row['item_name']. 
			 "', 'item_state': '".$row['item_state']."'},";
	}
	echo "]";
}

$conn->close();
?>