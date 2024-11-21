<?php
$servername = "localhost:3306";
$username = "root";
$password = "";
$dbname = "team4";

$loginUser = $_POST["username"];
$loginPass = $_POST["password"];

$conn = new mysqli($servername, $username, $password, $dbname);

$sql = "DELETE FROM acount WHERE id='$loginUser' AND pw='$loginPass'";
$result = $conn->query($sql);

if ($conn->affected_rows > 0) {
    echo "회원탈퇴가 완료";
} else {
    echo "회원탈퇴 실패";
}

$conn->close();
?>


