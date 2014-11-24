<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SimulateClient</title>
</head>
<body>
    <div>
        <form action="" method="post" enctype="multipart/form-data">
        <table>
            <tr>
                <td>
                    <em>请求的数据：</em>
                </td>
            </tr>
            <tr>
                <td>
                    <textarea name="txtData" rows="7" cols="100"><%=ViewData["ServiceRequest"]%></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    选择请求的Action：&nbsp;&nbsp;
                    <select name="ddlAction">
                        <option>
                            <%=ViewData["ServiceAction"]%>
                        </option>
                        <option>Login</option>
                        <option>AddKind</option>
                        <option>GetKindList</option>
                        <option>AddItem</option>
                        <option>GetAllItemList</option>
                        <option>GetKindItemList</option>
                        <option>GetHotItemList</option>
                        <option>GetLostItemList</option>
                        <option>GetUserItemList</option>
                        <option>AddBid</option>
                    </select>
                    &nbsp;&nbsp;
                    <input type="submit" value="请求" />
                </td>
            </tr>
            <tr>
                <td>
                    <em>返回的数据：</em>
                </td>
            </tr>
            <tr>
                <td>
                    <textarea name="txtResult" rows="21" cols="100"><%=ViewData["ServiceResponse"]%></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
