window.setDotnetHelper = function (dotnetHelper) {
    window.dotnetHelper = dotnetHelper;
};

window.onbeforeunload = function () {
    if (window.dotnetHelper) {
        window.dotnetHelper.invokeMethodAsync("ClearTemporalOrder");
    }
};