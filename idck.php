<?php                  
$servername = "localhost:3306"; 
$username = "root"; 
$password = ""; 
$dbname = "team4"; 

$inputUsername = $_POST['username'];

$conn = new mysqli($servername, $username, $password, $dbname);

$sql = "SELECT * FROM acount WHERE id = '$inputUsername'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "이미 사용 중인 아이디입니다.";
} else {
    echo "사용 가능한 아이디입니다.";
}

// 연결 종료
$conn->close();
?>

