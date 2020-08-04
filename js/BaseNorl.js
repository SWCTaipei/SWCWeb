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

function chknumber09(objElement) {
    var chrstring = objElement.value;
    var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
    if ((chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
        objElement.value = objElement.value.substring(0, chrstring.length - 1);
    }
}

function JsChkInputBox(r, e) { var t = r.value, a = t; switch (e) { case "01": var s = t.substring(t.length - 1, t.length); "0" != s && "1" != s && "2" != s && "3" != s && "4" != s && "5" != s && "6" != s && "7" != s && "8" != s && "9" != s && (r.value = r.value.substring(0, t.length - 1)); break; case "02": for (var S = 0; S < t.length; S++) sStr = t.charCodeAt(S), jTempStr = t.charAt(S), sStr > 47 && 58 > sStr || sStr > 64 && 91 > sStr || sStr > 96 && 123 > sStr || 45 == sStr || (a = a.replace(jTempStr, "")); r.value = a; break; case "03": break; case "N": jTempStr = "'", a = a.replace(jTempStr, "''") } }

function ChkRate(objElement) {
    var jsValue = objElement.value;
    var jsValLen = jsValue.substring(jsValue.length - 1, jsValue.length);
    if ((jsValLen != '0') && (jsValLen != '1') && (jsValLen != '2') && (jsValLen != '3') && (jsValLen != '4') && (jsValLen != '5') && (jsValLen != '6') && (jsValLen != '7') && (jsValLen != '8') && (jsValLen != '9') && (jsValLen != '.')) {
        objElement.value = objElement.value.substring(0, jsValue.length - 1);
    }
    if (isNaN(jsValue)) {
        objElement.value = '';
    } else {
        if (jsValue > 100) {
            objElement.value = 100;
        }
        if (jsValue < 0) {
            objElement.value = 0;
        }
    }
}
function checkID(id) {
    tab = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
    A1 = new Array(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3);
    A2 = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5);
    Mx = new Array(9, 8, 7, 6, 5, 4, 3, 2, 1, 1);

    if (id.length != 10) return false;
    i = tab.indexOf(id.charAt(0));
    if (i == -1) return false;
    sum = A1[i] + A2[i] * 9;

    for (i = 1; i < 10; i++) {
        v = parseInt(id.charAt(i));
        if (isNaN(v)) return false;
        sum = sum + v * Mx[i];
    }
    if (sum % 10 != 0) return false;
    return true;
}
