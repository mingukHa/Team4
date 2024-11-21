<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "team4";

$loginUser = $_POST["username"];
$loginPass = $_POST["password"];

$conn = new mysqli($servername, $username, $password, $dbname);

$sql = "INSERT INTO acount (id, pw) VALUES ('$loginUser', '$loginPass')";

if ($conn->query($sql) === TRUE) {
    echo "회원가입 성공!";
} else {
    echo "회원가입 실패: " . $conn->error;
}

$conn->close();
?>


