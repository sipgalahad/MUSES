var Constant = new (function () {
    this.StandardCode = new (function () {
        this.MARITAL_STATUS = "0002";
        this.ETHNIC = '0005';
        this.RELIGION = '0006';
        this.NATIONALITY = "0212";
        this.PROVINCE = "0347";
        this.OCCUPATION = "X012";
        this.EDUCATION = "X013";
        this.PATIENT_CATEGORY = "X067";
    })();

    this.CustomerType = new (function () {
        this.PERSONAL = "X004^999";
    })();

    this.PaymentType = new (function () {
        this.CASH = "X035^001";
        this.CREDIT_CARD = 'X035^002';
        this.DEBIT_CARD = 'X035^003';
    })();

    this.DosingFrequency = new (function () {
        this.HOUR = "X130^001";
        this.DAY = 'X130^002';
        this.WEEK = 'X130^999';
    })();

    this.DiscontinueMedicationReason = new (function () {
        this.OTHER = 'X136^999';
    })();

    this.DeleteReason = new (function () {
        this.OTHER = 'X129^999';
    })();

    this.RegistrationStatus = new (function () {
        this.CANCELLED = 'X020^006';
    })();

    this.FilterParameterType = new (function () {
        this.COMBO_BOX = "X108^001";
        this.CHECK_LIST = "X108^002";
        this.DATE = "X108^003";
        this.PAST_PERIOD = "X108^004";
        this.UPCOMING_PERIOD = "X108^005";
        this.FREE_TEXT = "X108^006";
        this.SEARCH_DIALOG = "X108^007";
        this.CUSTOM_COMBO_BOX = "X108^008";
        this.YEAR_COMBO_BOX = "X108^009";
        this.TEXT_BOX = "X108^010";
        this.CONSTANT = "X108^012";
    })();


    this.ToBePerformed = new (function () {
        this.CURRENT_EPISODE = "X125^001";
        this.PRIOR_TO_NEXT_VISIT = "X125^002";
        this.SCHEDULLED = "X125^003";
    })();

    this.AppointmentStatus = new (function () {
        this.COMPLETE = "0278^002";
        this.STARTED = "0278^008";
    })();

    

    this.Facility = new (function () {
        this.INPATIENT = "INPATIENT";
        this.EMERGENCY = "EMERGENCY";
        this.OUTPATIENT = "OUTPATIENT";
        this.DIAGNOSTIC = "DIAGNOSTIC";
    })();
})();