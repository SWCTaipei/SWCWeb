/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
    } else if (getStr == '承辦/監造技師') {
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
