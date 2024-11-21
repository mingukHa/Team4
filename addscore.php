<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "db_score";

$id = $_POST["id"];
$score = $_POST["score"];

$conn = new mysqli($servername, $username, $password, $dbname);



$sql = "SELECT * FROM tb_score WHERE id = '".$id."'";
$result = $conn->query($sql);

if($result->num_rows > 0){
	$update_sql = "UPDATE tb_score SET score = '".$score.
	"' WHERE id = '".$id."'";
	$conn->query($update_sql);
}
else{
	$insert_sql = 
	"INSERT INTO tb_score (id, score)
	VALUES ('".$id."', '".$score."')";
	$conn->query($insert_sql);
}
$conn->close();
?>