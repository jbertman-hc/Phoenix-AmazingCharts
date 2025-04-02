// MudBlazor initialization fix
window.mudElementRef = {
    addOnBlurEvent: function (element, dotNetRef) {
        if (element) {
            element.addEventListener('blur', function () {
                dotNetRef.invokeMethodAsync('OnBlur');
            });
        }
    },
    addOnFocusEvent: function (element, dotNetRef) {
        if (element) {
            element.addEventListener('focus', function () {
                dotNetRef.invokeMethodAsync('OnFocus');
            });
        }
    },
    addOnKeyDownEvent: function (element, dotNetRef) {
        if (element) {
            element.addEventListener('keydown', function (e) {
                dotNetRef.invokeMethodAsync('OnKeyDown', e.key, e.shiftKey, e.ctrlKey, e.altKey);
            });
        }
    },
    addOnKeyPressEvent: function (element, dotNetRef) {
        if (element) {
            element.addEventListener('keypress', function (e) {
                dotNetRef.invokeMethodAsync('OnKeyPress', e.key, e.shiftKey, e.ctrlKey, e.altKey);
            });
        }
    },
    addOnKeyUpEvent: function (element, dotNetRef) {
        if (element) {
            element.addEventListener('keyup', function (e) {
                dotNetRef.invokeMethodAsync('OnKeyUp', e.key, e.shiftKey, e.ctrlKey, e.altKey);
            });
        }
    }
};
