<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "team4";

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername,
				   $username,
				   $password,
				   $dbname);

$sql =
"SELECT * FROM acount WHERE id = '" . $loginUser . "'";
$result = $conn->query($sql);

if($result->num_rows > 0) {
	while($row = $result->fetch_assoc()) {
		if($row["pw"] == $loginPass) {
			echo "Login success";
			exit;
		}
	}
	echo "Wrong password..";
} else {
	echo "ID not found..";
}

$conn->close();
?>
