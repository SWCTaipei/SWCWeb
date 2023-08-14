
function showChange(e) {
    var getStr = e;

    document.getElementById('TXTID').value = '';
    document.getElementById('TXTPW').value = '';

    if (getStr == '水土保持義務人') {
        document.querySelector('#login2').textContent = '請輸入';
        document.querySelector('#login2').style.marginRight = '25px';
        document.querySelector('#login3').style.opacity = '0';
        document.getElementById('NewUser').style.display = 'none';
        document.getElementById('TXTID').placeholder = '身分證字號';
        document.getElementById('TXTPW').placeholder = '手機';
        document.getElementById('TXTPW').type = 'text';
        document.getElementById('TXTPW').type = 'password';
    } else if (getStr == '技師/各類委員') {
        document.querySelector('#login2').textContent = '帳號';
        document.querySelector('#login2').style.marginRight = '45px';
        document.querySelector('#login3').style.opacity = '1';
        document.getElementById('NewUser').style.display = 'inline-block';
        document.getElementById('TXTID').placeholder = '身分證字號';
        document.getElementById('TXTPW').placeholder = '';
        document.getElementById('TXTPW').type = 'password';
    } else if (getStr == '工務局大地工程處' || '審查/檢查單位') {
        document.querySelector('#login2').textContent = '帳號';
        document.querySelector('#login2').style.marginRight = '45px';
        document.querySelector('#login3').style.opacity = '1';
        document.getElementById('NewUser').style.display = 'none';
        document.getElementById('TXTID').placeholder = '';
        document.getElementById('TXTPW').placeholder = '';
        document.getElementById('TXTPW').type = 'password';
    }

   
}
