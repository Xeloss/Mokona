//Polyfills
Array.isArray || (Array.isArray = function(arg) {
    return Object.prototype.toString.call(arg) === '[object Array]';
})

Array.prototype.indexOf || (Array.prototype.indexOf = function (searchElement, fromIndex) {

    var k;

    if (this == null) {
        throw new TypeError('"this" is null or not defined');
    }

    var o = Object(this);

    var len = o.length >>> 0;
    if (len === 0) {
        return -1;
    }

    var n = +fromIndex || 0;

    if (Math.abs(n) === Infinity) {
        n = 0;
    }

    if (n >= len) {
        return -1;
    }
    k = Math.max(n >= 0 ? n : len - Math.abs(n), 0);

    while (k < len) {

        if (k in o && o[k] === searchElement) {
            return k;
        }
        k++;
    }
    return -1;
});

Array.prototype.find || (Array.prototype.find = function(predicate) {
    if (this == null) {
        throw new TypeError('Array.prototype.find called on null or undefined');
    }
    if (typeof predicate !== 'function') {
        throw new TypeError('predicate must be a function');
    }
    var list = Object(this);
    var length = list.length >>> 0;
    var thisArg = arguments[1];
    var value;

    for (var i = 0; i < length; i++) {
        value = list[i];
        if (predicate.call(thisArg, value, i, list)) {
            return value;
        }
    }
    return undefined;
});

Array.prototype.filter || (Array.prototype.filter = function (fun/*, thisArg*/) {
    'use strict';

    if (this === void 0 || this === null) {
        throw new TypeError();
    }

    var t = Object(this);
    var len = t.length >>> 0;
    if (typeof fun !== 'function') {
        throw new TypeError();
    }

    var res = [];
    var thisArg = arguments.length >= 2 ? arguments[1] : void 0;
    for (var i = 0; i < len; i++) {
        if (i in t) {
            var val = t[i];

            // NOTE: Technically this should Object.defineProperty at
            //       the next index, as push can be affected by
            //       properties on Object.prototype and Array.prototype.
            //       But that method's new, and collisions should be
            //       rare, so use the more-compatible alternative.
            if (fun.call(thisArg, val, i, t)) {
                res.push(val);
            }
        }
    }

    return res;
});

Array.prototype.forEach || (Array.prototype.forEach = function forEach(callback, thisArg) {
    'use strict';
    var T, k;

    if (this == null) {
        throw new TypeError("this is null or not defined");
    }

    var kValue,
        // 1. Let O be the result of calling ToObject passing the |this| value as the argument.
        O = Object(this),

        // 2. Let lenValue be the result of calling the Get internal method of O with the argument "length".
        // 3. Let len be ToUint32(lenValue).
        len = O.length >>> 0; // Hack to convert O.length to a UInt32

    // 4. If IsCallable(callback) is false, throw a TypeError exception.
    // See: http://es5.github.com/#x9.11
    if ({}.toString.call(callback) !== "[object Function]") {
        throw new TypeError(callback + " is not a function");
    }

    // 5. If thisArg was supplied, let T be thisArg; else let T be undefined.
    if (arguments.length >= 2) {
        T = thisArg;
    }

    // 6. Let k be 0
    k = 0;

    // 7. Repeat, while k < len
    while (k < len) {

        // a. Let Pk be ToString(k).
        //   This is implicit for LHS operands of the in operator
        // b. Let kPresent be the result of calling the HasProperty internal method of O with argument Pk.
        //   This step can be combined with c
        // c. If kPresent is true, then
        if (k in O) {

            // i. Let kValue be the result of calling the Get internal method of O with argument Pk.
            kValue = O[k];

            // ii. Call the Call internal method of callback with T as the this value and
            // argument list containing kValue, k, and O.
            callback.call(T, kValue, k, O);
        }
        // d. Increase k by 1.
        k++;
    }
    // 8. return undefined
});

Array.prototype.reduce || (Array.prototype.reduce = function(callback /*, initialValue*/) {
    'use strict';
    if (this === null) {
        throw new TypeError('Array.prototype.reduce called on null or undefined');
    }
    if (typeof callback !== 'function') {
        throw new TypeError(callback + ' is not a function');
    }
    var t = Object(this), len = t.length >>> 0, k = 0, value;
    if (arguments.length == 2) {
        value = arguments[1];
    } else {
        while (k < len && !(k in t)) {
            k++; 
        }
        if (k >= len) {
            throw new TypeError('Reduce of empty array with no initial value');
        }
        value = t[k++];
    }
    for (; k < len; k++) {
        if (k in t) {
            value = callback(value, t[k], k, t);
        }
    }
    return value;
});

Object.keys || (Object.keys = (function () {
    'use strict';
    var hasOwnProperty = Object.prototype.hasOwnProperty,
        hasDontEnumBug = !({ toString: null }).propertyIsEnumerable('toString'),
        dontEnums = [
          'toString',
          'toLocaleString',
          'valueOf',
          'hasOwnProperty',
          'isPrototypeOf',
          'propertyIsEnumerable',
          'constructor'
        ],
        dontEnumsLength = dontEnums.length;

    return function (obj) {
        if (typeof obj !== 'object' && (typeof obj !== 'function' || obj === null)) {
            throw new TypeError('Object.keys called on non-object');
        }

        var result = [], prop, i;

        for (prop in obj) {
            if (hasOwnProperty.call(obj, prop)) {
                result.push(prop);
            }
        }

        if (hasDontEnumBug) {
            for (i = 0; i < dontEnumsLength; i++) {
                if (hasOwnProperty.call(obj, dontEnums[i])) {
                    result.push(dontEnums[i]);
                }
            }
        }
        return result;
    };
}()));

//Customs
Function.isFunction || (Function.isFunction = function (object) {
    return object && Object.prototype.toString.call(object) === '[object Function]';
})

Array.prototype.removeAt || (Array.prototype.removeAt = function (index) {
    if (index > -1 && index < this.length)
        this.splice(index, 1);
});

Array.prototype.remove || (Array.prototype.remove = function (elements) {

    if (Array.isArray(elements)) {
        elements.forEach(function () { this.removeAt(this.indexOf(elements)) });
    }
    else {
        this.removeAt(this.indexOf(elements));
    }
});

Array.prototype.removeWhere || (Array.prototype.removeWhere = function (criteria, thisArg) {

    var indexesToRemove = [];
    var criteriaThis;

    if (arguments.length >= 2) {
        criteriaThis = thisArg;
    }

    for (var i = 0; i < this.length; i++) {
        if (criteria.call(criteriaThis, this[i], i))
            indexesToRemove.push(i);
    }

    indexesToRemove.forEach(this.removeAt);
});

Array.prototype.groupBy || (Array.prototype.groupBy = function (keyExpression/*, equalityComparer*/) {

    var result = [];

    var equalityComparer = arguments.length >= 2 ? arguments[1] : function (a, b) { return a == b; };

    this.forEach(function (entry) {

        var key = Function.isFunction(keyExpression) ? keyExpression(entry) : entry[keyExpression];

        var group = result.find(function (r) { return equalityComparer(key, r.key); });
        if (!group) {
            group = { key: key, values: [] };
            result.push(group);
        }

        group.values.push(entry);
    });

    return result;
});

Array.prototype.firstOrDefault || (Array.prototype.groupBy = function (criteria) {

    if (this.length == 0)
        return null;

    if (!Function.isFunction(criteria))
        return this[0];

    for (var i = 0; i < this.length; i++) {
        if (criteria(this[i]))
            return this[i];
    }

    return null;
   
});