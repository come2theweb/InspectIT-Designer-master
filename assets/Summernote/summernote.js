! function (n) {
	function e(r) {
		if (t[r]) return t[r].exports;
		var o = t[r] = {
			i: r,
			l: !1,
			exports: {}
		};
		return n[r].call(o.exports, o, o.exports, e), o.l = !0, o.exports
	}
	var t = {};
	e.m = n, e.c = t, e.d = function (n, t, r) {
		e.o(n, t) || Object.defineProperty(n, t, {
			configurable: !1,
			enumerable: !0,
			get: r
		})
	}, e.n = function (n) {
		var t = n && n.__esModule ? function () {
			return n.default
		} : function () {
			return n
		};
		return e.d(t, "a", t), t
	}, e.o = function (n, e) {
		return Object.prototype.hasOwnProperty.call(n, e)
	}, e.p = "", e(e.s = 36)
}({
	36: function (n, e, t) {
		n.exports = t(37)
	},
	37: function (n, e) {
		! function (n) {
			n.fn.APSummernote = function () {
				this.length && void 0 !== n.fn.summernote && this.summernote({
					popover: {
						image: [],
						link: [],
						air: []
					},
					dialogsInBody: !0
				})
			}, n("#summernote").APSummernote()
		}(jQuery)
	}
});