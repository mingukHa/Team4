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

$sql = "SELECT a.id, inv.inventory_num, i.item_num, i.item_name, i.item_state
FROM account a
JOIN inventory inv ON a.id = inv.id
JOIN item i ON inv.item_num = i.item_num
WHERE a.id = 'aaa';";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	echo "[";
	while($row = $result->fetch_assoc()) {
		echo "{'id': '".$row['id'].
			 "', 'inventory_num': '".$row['inventory_num'].
			 "', 'item_num': '".$row['item_num'].
			 "', 'item_name': '".$row['item_name']. 
			 "', 'item_state': '".$row['item_state']."'}";
	}
	echo "]";
}

$conn->close();
?>