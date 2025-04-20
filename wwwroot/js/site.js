$(document).ready(function () {
    $("#bookingForm, #statusForm").each(function () {
        $(this).validate({
            rules: {
                Name: { required: true, maxlength: 100 },
                Email: { required: true, email: true },
                Facility: { required: true },
                BookingDate: {
                    required: true,
                    date: true,
                    min: new Date().toISOString().split("T")[0]
                },
                StartTime: { required: true },
                EndTime: { required: true }
            },
            messages: {
                Name: {
                    required: "Please enter your name.",
                    maxlength: "Name cannot exceed 100 characters."
                },
                Email: {
                    required: "Please enter your email.",
                    email: "Please enter a valid email address."
                },
                Facility: "Please select a facility.",
                BookingDate: {
                    required: "Please select a date.",
                    min: "Booking date cannot be in the past."
                },
                StartTime: "Please select a start time.",
                EndTime: "Please select an end time."
            },
            submitHandler: function (form) {
                if (form.id === "bookingForm") {
                    var startTime = $("#StartTime").val();
                    var endTime = $("#EndTime").val();
                    if (startTime >= endTime) {
                        alert("End time must be after start time.");
                        return false;
                    }
                }
                form.submit();
            }
        });
    });
});