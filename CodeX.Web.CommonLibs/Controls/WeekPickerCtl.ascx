<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeekPickerCtl.ascx.cs" 
    Inherits="CodeX.Web.CommonLibs.Controls.WeekPickerCtl" %>
   
<script type="text/javascript" id="dxss_weekpickerctl">
    $(function () {
        var startDate;
        var endDate;

        var selectCurrentWeek = function () {
            window.setTimeout(function () {
                $('.week-picker').find('.ui-datepicker-current-day a').addClass('ui-state-active')
                $('.week-picker').hide();
            }, 1);
        }

        $('.week-picker').datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            dateFormat: "dd-mm-yy",
            onSelect: function (dateText, inst) {
                var date = $(this).datepicker('getDate');
                startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;

                var startDate = $.datepicker.formatDate(dateFormat, startDate, inst.settings);
                var endDate = $.datepicker.formatDate(dateFormat, endDate, inst.settings);
                $('#<%=hdnStartDate.ClientID %>').val(startDate);
                $('#<%=hdnEndDate.ClientID %>').val(endDate);

                $('#<%=txtDate.ClientID %>').val(startDate + ' - ' + endDate);

                selectCurrentWeek();
                cbpView.PerformCallback('refresh');
            },
            beforeShowDay: function (date) {
                var cssClass = '';
                if (date >= startDate && date <= endDate)
                    cssClass = 'ui-datepicker-current-day';
                return [true, cssClass];
            },
            onChangeMonthYear: function (year, month, inst) {
                selectCurrentWeek();
            }
        });

        $('#imgPickDate').click(function () {
            if ($('.week-picker').is(":visible"))
                $('.week-picker').hide();
            else
                $('.week-picker').show();
        });

        $('.week-picker .ui-datepicker-calendar tr').live('mousemove', function () { $(this).find('td a').addClass('ui-state-hover'); });
        $('.week-picker .ui-datepicker-calendar tr').live('mouseleave', function () { $(this).find('td a').removeClass('ui-state-hover'); });
    });
</script>
<input type="hidden" id="hdnStartDate" runat="server" />
<input type="hidden" id="hdnEndDate" runat="server" />
<table cellpadding="0" cellspacing="0">
    <tr>
        <td><asp:TextBox ID="txtDate" runat="server" Width="200px" Style="text-align: center" ReadOnly="true" /></td>
        <td>&nbsp;</td>
        <td><img src='<%=ResolveUrl("~/Libs/Images/calendar.gif") %>' id="imgPickDate" class="imgLink" /></td>
    </tr>
</table>
<div style="position: relative;">
    <div style="position: absolute;z-index: 10000">
        <div class="week-picker" style="display:none;"></div>    
    </div>
</div>