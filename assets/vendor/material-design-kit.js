! function (t, e) {
	"object" == typeof exports && "object" == typeof module ? module.exports = e(require("dom-factory")) : "function" == typeof define && define.amd ? define(["dom-factory"], e) : "object" == typeof exports ? exports.MDK = e(require("dom-factory")) : t.MDK = e(t.domFactory)
}(this, function (t) {
	return function (t) {
		function e(n) {
			if (i[n]) return i[n].exports;
			var r = i[n] = {
				i: n,
				l: !1,
				exports: {}
			};
			return t[n].call(r.exports, r, r.exports, e), r.l = !0, r.exports
		}
		var i = {};
		return e.m = t, e.c = i, e.d = function (t, i, n) {
			e.o(t, i) || Object.defineProperty(t, i, {
				configurable: !1,
				enumerable: !0,
				get: n
			})
		}, e.n = function (t) {
			var i = t && t.__esModule ? function () {
				return t.default
			} : function () {
				return t
			};
			return e.d(i, "a", i), i
		}, e.o = function (t, e) {
			return Object.prototype.hasOwnProperty.call(t, e)
		}, e.p = "", e(e.s = 25)
	}([function (e, i) {
		e.exports = t
	}, function (t, e, i) {
		"use strict";
		var n = i(2),
			r = function () {
				return {
					_scrollTargetSelector: null,
					_scrollTarget: null,
					get scrollTarget() {
						return this._scrollTarget ? this._scrollTarget : this._defaultScrollTarget
					},
					set scrollTarget(t) {
						this._scrollTarget = t
					},
					get scrollTargetSelector() {
						return this._scrollTargetSelector ? this._scrollTargetSelector : this.element.hasAttribute("data-scroll-target") ? this.element.getAttribute("data-scroll-target") : void 0
					},
					set scrollTargetSelector(t) {
						this._scrollTargetSelector = t
					},
					get _defaultScrollTarget() {
						return this._doc
					},
					get _owner() {
						return this.element.ownerDocument
					},
					get _doc() {
						return this._owner.documentElement
					},
					get _scrollTop() {
						return this._isValidScrollTarget() ? this.scrollTarget === this._doc ? window.pageYOffset : this.scrollTarget.scrollTop : 0
					},
					set _scrollTop(t) {
						this.scrollTarget === this._doc ? window.scrollTo(window.pageXOffset, t) : this._isValidScrollTarget() && (this.scrollTarget.scrollTop = t)
					},
					get _scrollLeft() {
						return this._isValidScrollTarget() ? this.scrollTarget === this._doc ? window.pageXOffset : this.scrollTarget.scrollLeft : 0
					},
					set _scrollLeft(t) {
						this.scrollTarget === this._doc ? window.scrollTo(t, window.pageYOffset) : this._isValidScrollTarget() && (this.scrollTarget.scrollLeft = t)
					},
					get _scrollTargetWidth() {
						return this._isValidScrollTarget() ? this.scrollTarget === this._doc ? window.innerWidth : this.scrollTarget.offsetWidth : 0
					},
					get _scrollTargetHeight() {
						return this._isValidScrollTarget() ? this.scrollTarget === this._doc ? window.innerHeight : this.scrollTarget.offsetHeight : 0
					},
					get _isPositionedFixed() {
						return this.element instanceof HTMLElement && "fixed" === window.getComputedStyle(this.element).position
					},
					attachToScrollTarget: function () {
						this.detachFromScrollTarget(), Object(n.watch)(this, "scrollTargetSelector", this.attachToScrollTarget), "document" === this.scrollTargetSelector ? this.scrollTarget = this._doc : "string" == typeof this.scrollTargetSelector ? this.scrollTarget = document.querySelector("" + this.scrollTargetSelector) : this.scrollTargetSelector instanceof HTMLElement && (this.scrollTarget = this.scrollTargetSelector), this._doc.style.overflow || (this._doc.style.overflow = this.scrollTarget !== this._doc ? "hidden" : ""), this.scrollTarget && (this.eventTarget = this.scrollTarget === this._doc ? window : this.scrollTarget, this._boundScrollHandler = this._boundScrollHandler || this._scrollHandler.bind(this), this.eventTarget.addEventListener("scroll", this._boundScrollHandler))
					},
					detachFromScrollTarget: function () {
						Object(n.unwatch)(this, "scrollTargetSelector", this.attachToScrollTarget), this.eventTarget && this.eventTarget.removeEventListener("scroll", this._boundScrollHandler)
					},
					scroll: function () {
						var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : 0,
							e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0;
						this.scrollTarget === this._doc ? window.scrollTo(t, e) : this._isValidScrollTarget() && (this.scrollTarget.scrollLeft = t, this.scrollTarget.scrollTop = e)
					},
					scrollWithBehavior: function () {
						var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : 0,
							e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : 0,
							i = arguments[2],
							n = arguments[3];
						if (n = "function" == typeof n ? n : function (t, e, i, n) {
							return t /= n, -i * t * (t - 2) + e
						}, "smooth" === i) {
							var r = Date.now(),
								o = this._scrollTop,
								s = this._scrollLeft,
								a = e - o,
								l = t - s;
							(function t() {
								var e = Date.now(),
									i = e - r;
								i < 300 && (this.scroll(n(i, s, l, 300), n(i, o, a, 300)), requestAnimationFrame(t.bind(this)))
							}).call(this)
						} else this.scroll(t, e)
					},
					_isValidScrollTarget: function () {
						return this.scrollTarget instanceof HTMLElement
					},
					_scrollHandler: function () { }
				}
			};
		i.d(e, "a", function () {
			return r
		})
	}, function (t, e, i) {
		! function (e, i) {
			t.exports = function () {
				return function (t) {
					function e(n) {
						if (i[n]) return i[n].exports;
						var r = i[n] = {
							exports: {},
							id: n,
							loaded: !1
						};
						return t[n].call(r.exports, r, r.exports, e), r.loaded = !0, r.exports
					}
					var i = {};
					return e.m = t, e.c = i, e.p = "", e(0)
				}([function (t, e, i) {
					"use strict";

					function n(t) {
						return t && t.__esModule ? t : {
							default: t
						}
					}
					Object.defineProperty(e, "__esModule", {
						value: !0
					}), e.unwatch = e.watch = void 0;
					var r = i(4),
						o = n(r),
						s = i(3),
						a = n(s),
						l = (e.watch = function () {
							for (var t = arguments.length, e = Array(t), i = 0; t > i; i++) e[i] = arguments[i];
							var n = e[1];
							h(n) ? y.apply(void 0, e) : l(n) ? b.apply(void 0, e) : v.apply(void 0, e)
						}, e.unwatch = function () {
							for (var t = arguments.length, e = Array(t), i = 0; t > i; i++) e[i] = arguments[i];
							var n = e[1];
							h(n) || void 0 === n ? x.apply(void 0, e) : l(n) ? T.apply(void 0, e) : w.apply(void 0, e)
						}, function (t) {
							return "[object Array]" === {}.toString.call(t)
						}),
						c = function (t) {
							return "[object Object]" === {}.toString.call(t)
						},
						h = function (t) {
							return "[object Function]" === {}.toString.call(t)
						},
						u = function (t, e, i) {
							(0, a.default)(t, e, {
								enumerable: !1,
								configurable: !0,
								writable: !1,
								value: i
							})
						},
						f = function (t, e, i, n) {
							(0, a.default)(t, e, {
								get: i,
								set: function (t) {
									n.call(this, t)
								},
								enumerable: !0,
								configurable: !0
							})
						},
						d = function (t, e, i, n, r) {
							var o = void 0,
								s = t.__watchers__[e];
							(o = t.__watchers__.__watchall__) && (s = s ? s.concat(o) : o);
							for (var a = s ? s.length : 0, l = 0; a > l; l++) s[l].call(t, i, n, e, r)
						},
						_ = ["pop", "push", "reverse", "shift", "sort", "unshift", "splice"],
						m = function (t, e, i, n) {
							u(t, i, function () {
								for (var r = 0, o = void 0, s = void 0, a = arguments.length, l = Array(a), c = 0; a > c; c++) l[c] = arguments[c];
								if ("splice" === i) {
									var h = l[0],
										u = h + l[1];
									o = t.slice(h, u), s = [];
									for (var f = 2; f < l.length; f++) s[f - 2] = l[f];
									r = h
								} else s = "push" === i || "unshift" === i ? l.length > 0 ? l : void 0 : l.length > 0 ? l[0] : void 0;
								var d = e.apply(t, l);
								return "pop" === i ? (o = d, r = t.length) : "push" === i ? r = t.length - 1 : "shift" === i ? o = d : "unshift" !== i && void 0 === s && (s = d), n.call(t, r, i, s, o), d
							})
						},
						g = function (t, e) {
							if (h(e) && t && !(t instanceof String) && l(t))
								for (var i = _.length; i > 0; i--) {
									var n = _[i - 1];
									m(t, t[n], n, e)
								}
						},
						p = function (t, e, i, n) {
							var r = !1,
								s = l(t);
							void 0 === t.__watchers__ && (u(t, "__watchers__", {}), s && g(t, function (i, r, o, s) {
								if (d(t, i, o, s, r), 0 !== n && o && (c(o) || l(o))) {
									var a = void 0,
										h = t.__watchers__[e];
									(a = t.__watchers__.__watchall__) && (h = h ? h.concat(a) : a);
									for (var u = h ? h.length : 0, f = 0; u > f; f++)
										if ("splice" !== r) y(o, h[f], void 0 === n ? n : n - 1);
										else
											for (var _ = 0; _ < o.length; _++) y(o[_], h[f], void 0 === n ? n : n - 1)
								}
							})), void 0 === t.__proxy__ && u(t, "__proxy__", {}), void 0 === t.__watchers__[e] && (t.__watchers__[e] = [], s || (r = !0));
							for (var h = 0; h < t.__watchers__[e].length; h++)
								if (t.__watchers__[e][h] === i) return;
							t.__watchers__[e].push(i), r && function () {
								var i = (0, o.default)(t, e);
								void 0 !== i ? function () {
									var n = {
										enumerable: i.enumerable,
										configurable: i.configurable
									};
									["get", "set"].forEach(function (e) {
										void 0 !== i[e] && (n[e] = function () {
											for (var n = arguments.length, r = Array(n), o = 0; n > o; o++) r[o] = arguments[o];
											return i[e].apply(t, r)
										})
									}), ["writable", "value"].forEach(function (t) {
										void 0 !== i[t] && (n[t] = i[t])
									}), (0, a.default)(t.__proxy__, e, n)
								}() : t.__proxy__[e] = t[e], f(t, e, function () {
									return t.__proxy__[e]
								}, function (i) {
									var r = t.__proxy__[e];
									if (0 !== n && t[e] && (c(t[e]) || l(t[e])) && !t[e].__watchers__)
										for (var o = 0; o < t.__watchers__[e].length; o++) y(t[e], t.__watchers__[e][o], void 0 === n ? n : n - 1);
									r !== i && (t.__proxy__[e] = i, d(t, e, i, r, "set"))
								})
							}()
						},
						y = function t(e, i, n) {
							if ("string" != typeof e && (e instanceof Object || l(e)))
								if (l(e)) {
									if (p(e, "__watchall__", i, n), void 0 === n || n > 0)
										for (var r = 0; r < e.length; r++) t(e[r], i, n)
								} else {
									var o = [];
									for (var s in e) ({}).hasOwnProperty.call(e, s) && o.push(s);
									b(e, o, i, n)
								}
						},
						v = function (t, e, i, n) {
							"string" != typeof t && (t instanceof Object || l(t)) && (h(t[e]) || (null !== t[e] && (void 0 === n || n > 0) && y(t[e], i, void 0 !== n ? n - 1 : n), p(t, e, i, n)))
						},
						b = function (t, e, i, n) {
							if ("string" != typeof t && (t instanceof Object || l(t)))
								for (var r = 0; r < e.length; r++) {
									var o = e[r];
									v(t, o, i, n)
								}
						},
						w = function (t, e, i) {
							if (void 0 !== t.__watchers__ && void 0 !== t.__watchers__[e])
								if (void 0 === i) delete t.__watchers__[e];
								else
									for (var n = 0; n < t.__watchers__[e].length; n++) t.__watchers__[e][n] === i && t.__watchers__[e].splice(n, 1)
						},
						T = function (t, e, i) {
							for (var n in e) e.hasOwnProperty(n) && w(t, e[n], i)
						},
						S = function t(e, i) {
							var n = [];
							for (var r in e) e.hasOwnProperty(r) && (e[r] instanceof Object && t(e[r], i), n.push(r));
							T(e, n, i)
						},
						x = function (t, e) {
							if (!(t instanceof String || !t instanceof Object && !l(t)))
								if (l(t)) {
									for (var i = ["__watchall__"], n = 0; n < t.length; n++) i.push(n);
									T(t, i, e)
								} else S(t, e)
						}
				}, function (t, e) {
					var i = t.exports = {
						version: "1.2.6"
					};
					"number" == typeof __e && (__e = i)
				}, function (t, e) {
					var i = Object;
					t.exports = {
						create: i.create,
						getProto: i.getPrototypeOf,
						isEnum: {}.propertyIsEnumerable,
						getDesc: i.getOwnPropertyDescriptor,
						setDesc: i.defineProperty,
						setDescs: i.defineProperties,
						getKeys: i.keys,
						getNames: i.getOwnPropertyNames,
						getSymbols: i.getOwnPropertySymbols,
						each: [].forEach
					}
				}, function (t, e, i) {
					t.exports = {
						default: i(5),
						__esModule: !0
					}
				}, function (t, e, i) {
					t.exports = {
						default: i(6),
						__esModule: !0
					}
				}, function (t, e, i) {
					var n = i(2);
					t.exports = function (t, e, i) {
						return n.setDesc(t, e, i)
					}
				}, function (t, e, i) {
					var n = i(2);
					i(17), t.exports = function (t, e) {
						return n.getDesc(t, e)
					}
				}, function (t, e) {
					t.exports = function (t) {
						if ("function" != typeof t) throw TypeError(t + " is not a function!");
						return t
					}
				}, function (t, e) {
					var i = {}.toString;
					t.exports = function (t) {
						return i.call(t).slice(8, -1)
					}
				}, function (t, e, i) {
					var n = i(7);
					t.exports = function (t, e, i) {
						if (n(t), void 0 === e) return t;
						switch (i) {
							case 1:
								return function (i) {
									return t.call(e, i)
								};
							case 2:
								return function (i, n) {
									return t.call(e, i, n)
								};
							case 3:
								return function (i, n, r) {
									return t.call(e, i, n, r)
								}
						}
						return function () {
							return t.apply(e, arguments)
						}
					}
				}, function (t, e) {
					t.exports = function (t) {
						if (void 0 == t) throw TypeError("Can't call method on  " + t);
						return t
					}
				}, function (t, e, i) {
					var n = i(13),
						r = i(1),
						o = i(9),
						s = "prototype",
						a = function (t, e, i) {
							var l, c, h, u = t & a.F,
								f = t & a.G,
								d = t & a.S,
								_ = t & a.P,
								m = t & a.B,
								g = t & a.W,
								p = f ? r : r[e] || (r[e] = {}),
								y = f ? n : d ? n[e] : (n[e] || {})[s];
							f && (i = e);
							for (l in i) (c = !u && y && l in y) && l in p || (h = c ? y[l] : i[l], p[l] = f && "function" != typeof y[l] ? i[l] : m && c ? o(h, n) : g && y[l] == h ? function (t) {
								var e = function (e) {
									return this instanceof t ? new t(e) : t(e)
								};
								return e[s] = t[s], e
							}(h) : _ && "function" == typeof h ? o(Function.call, h) : h, _ && ((p[s] || (p[s] = {}))[l] = h))
						};
					a.F = 1, a.G = 2, a.S = 4, a.P = 8, a.B = 16, a.W = 32, t.exports = a
				}, function (t, e) {
					t.exports = function (t) {
						try {
							return !!t()
						} catch (t) {
							return !0
						}
					}
				}, function (t, e) {
					var i = t.exports = "undefined" != typeof window && window.Math == Math ? window : "undefined" != typeof self && self.Math == Math ? self : Function("return this")();
					"number" == typeof __g && (__g = i)
				}, function (t, e, i) {
					var n = i(8);
					t.exports = Object("z").propertyIsEnumerable(0) ? Object : function (t) {
						return "String" == n(t) ? t.split("") : Object(t)
					}
				}, function (t, e, i) {
					var n = i(11),
						r = i(1),
						o = i(12);
					t.exports = function (t, e) {
						var i = (r.Object || {})[t] || Object[t],
							s = {};
						s[t] = e(i), n(n.S + n.F * o(function () {
							i(1)
						}), "Object", s)
					}
				}, function (t, e, i) {
					var n = i(14),
						r = i(10);
					t.exports = function (t) {
						return n(r(t))
					}
				}, function (t, e, i) {
					var n = i(16);
					i(15)("getOwnPropertyDescriptor", function (t) {
						return function (e, i) {
							return t(n(e), i)
						}
					})
				}])
			}()
		}()
	}, function (t, e, i) {
		"use strict";
		var n = i(0),
			r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) {
				return typeof t
			} : function (t) {
				return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
			},
			o = function () {
				return {
					_scrollEffects: {},
					_effectsRunFn: [],
					_effects: [],
					_effectsConfig: null,
					get effects() {
						return this.element.dataset.effects || []
					},
					get effectsConfig() {
						if (this._effectsConfig) return this._effectsConfig;
						if (this.element.hasAttribute("data-effects-config")) try {
							return JSON.parse(this.element.getAttribute("data-effects-config"))
						} catch (t) { }
						return {}
					},
					set effectsConfig(t) {
						this._effectsConfig = t
					},
					get _clampedScrollTop() {
						return Math.max(0, this._scrollTop)
					},
					registerEffect: function (t, e) {
						if (void 0 !== this._scrollEffects[t]) throw new Error("effect " + t + " is already registered.");
						this._scrollEffects[t] = e
					},
					isOnScreen: function () {
						return !1
					},
					isContentBelow: function () {
						return !1
					},
					createEffect: function (t) {
						var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {},
							i = this._scrollEffects[t];
						if (void 0 === (void 0 === i ? "undefined" : r(i))) throw new ReferenceError("Scroll effect " + t + " was not registered");
						var n = this._boundEffect(i, e);
						return n.setUp(), n
					},
					_boundEffect: function (t) {
						var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {},
							i = parseFloat(e.startsAt || 0),
							n = parseFloat(e.endsAt || 1),
							r = n - i,
							o = Function(),
							s = 0 === i && 1 === n ? t.run : function (e, n) {
								t.run.call(this, Math.max(0, (e - i) / r), n)
							};
						return {
							setUp: t.setUp ? t.setUp.bind(this, e) : o,
							run: t.run ? s.bind(this) : o,
							tearDown: t.tearDown ? t.tearDown.bind(this) : o
						}
					},
					_setUpEffects: function () {
						var t = this;
						this._tearDownEffects(), this.effects.forEach(function (e) {
							var i = void 0;
							(i = t._scrollEffects[e]) && t._effects.push(t._boundEffect(i, t.effectsConfig[e]))
						}), this._effects.forEach(function (e) {
							!1 !== e.setUp() && t._effectsRunFn.push(e.run)
						})
					},
					_tearDownEffects: function () {
						this._effects.forEach(function (t) {
							t.tearDown()
						}), this._effectsRunFn = [], this._effects = []
					},
					_runEffects: function (t, e) {
						this._effectsRunFn.forEach(function (i) {
							return i(t, e)
						})
					},
					_scrollHandler: function () {
						this._updateScrollState(this._clampedScrollTop)
					},
					_updateScrollState: function (t) { },
					_transform: function (t, e) {
						e = e || this.element, n.util.transform(t, e)
					}
				}
			};
		i.d(e, "a", function () {
			return o
		})
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			if (Array.isArray(t)) {
				for (var e = 0, i = Array(t.length); e < t.length; e++) i[e] = t[e];
				return i
			}
			return Array.from(t)
		}
		var r = {
			name: "blend-background",
			setUp: function () {
				var t = this,
					e = this.element.querySelector('[class*="__bg-front"]'),
					i = this.element.querySelector('[class*="__bg-rear"]');
				[e, i].map(function (e) {
					e && "" === e.style.transform && (t._transform("translateZ(0)", e), e.style.willChange = "opacity")
				}), i.style.opacity = 0
			},
			run: function (t, e) {
				var i = this.element.querySelector('[class*="__bg-front"]'),
					n = this.element.querySelector('[class*="__bg-rear"]');
				i.style.opacity = (1 - t).toFixed(5), n.style.opacity = t.toFixed(5)
			}
		},
			o = {
				name: "fade-background",
				setUp: function (t) {
					var e = this,
						i = t.duration || "0.5s",
						r = t.threshold || (this._isPositionedFixed ? 1 : .3);
					[this.element.querySelector('[class*="__bg-front"]'), this.element.querySelector('[class*="__bg-rear"]')].map(function (t) {
						if (t) {
							var r = t.style.willChange.split(",").map(function (t) {
								return t.trim()
							}).filter(function (t) {
								return t.length
							});
							r.push("opacity", "transform"), t.style.willChange = [].concat(n(new Set(r))).join(", "), "" === t.style.transform && e._transform("translateZ(0)", t), t.style.transitionProperty = "opacity", t.style.transitionDuration = i
						}
					}), this._fadeBackgroundThreshold = this._isPositionedFixed ? r : r + this._progress * r
				},
				tearDown: function () {
					delete this._fadeBackgroundThreshold
				},
				run: function (t, e) {
					var i = this.element.querySelector('[class*="__bg-front"]'),
						n = this.element.querySelector('[class*="__bg-rear"]');
					t >= this._fadeBackgroundThreshold ? (i.style.opacity = 0, n.style.opacity = 1) : (i.style.opacity = 1, n.style.opacity = 0)
				}
			},
			s = {
				name: "parallax-background",
				setUp: function () { },
				tearDown: function () {
					var t = this,
						e = ["marginTop", "marginBottom"];
					[this.element.querySelector('[class*="__bg-front"]'), this.element.querySelector('[class*="__bg-rear"]')].map(function (i) {
						i && (t._transform("translate3d(0, 0, 0)", i), e.forEach(function (t) {
							return i.style[t] = ""
						}))
					})
				},
				run: function (t, e) {
					var i = this,
						n = (this.scrollTarget.scrollHeight - this._scrollTargetHeight) / this.scrollTarget.scrollHeight,
						r = this.element.offsetHeight * n;
					void 0 !== this._dHeight && (n = this._dHeight / this.element.offsetHeight, r = this._dHeight * n);
					var o = Math.abs(.5 * r).toFixed(5),
						s = this._isPositionedFixedEmulated ? 1e6 : r,
						a = o * t,
						l = Math.min(a, s).toFixed(5);
					[this.element.querySelector('[class*="__bg-front"]'), this.element.querySelector('[class*="__bg-rear"]')].map(function (t) {
						t && (t.style.marginTop = -1 * o + "px", i._transform("translate3d(0, " + l + "px, 0)", t))
					});
					var c = this.element.querySelector('[class$="__bg"]');
					c.style.visibility || (c.style.visibility = "visible")
				}
			};
		i.d(e, "a", function () {
			return a
		}), i.d(e, !1, function () {
			return r
		}), i.d(e, !1, function () {
			return o
		}), i.d(e, !1, function () {
			return s
		});
		var a = [r, o, s]
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			if (Array.isArray(t)) {
				for (var e = 0, i = Array(t.length); e < t.length; e++) i[e] = t[e];
				return i
			}
			return Array.from(t)
		}
		var r = {
			name: "waterfall",
			run: function (t, e) {
				this.element.classList[this.isOnScreen() && this.isContentBelow() ? "add" : "remove"]("mdk-header--shadow")
			},
			tearDown: function () {
				this.element.classList.remove("mdk-header--shadow")
			}
		},
			o = function (t, e, i, n) {
				i.apply(n, e.map(function (e) {
					return e[0] + (e[1] - e[0]) * t
				}))
			},
			s = {
				name: "fx-condenses",
				setUp: function () {
					var t = this,
						e = [].concat(n(this.element.querySelectorAll("[fx-condenses]"))),
						i = [].concat(n(this.element.querySelectorAll("[fx-id]"))),
						r = {};
					e.forEach(function (e) {
						if (e) {
							e.style.willChange = "transform", t._transform("translateZ(0)", e), "inline" === window.getComputedStyle(e).display && (e.style.display = "inline-block");
							var i = e.getAttribute("id");
							e.hasAttribute("id") || (i = "rt" + (0 | 9e6 * Math.random()).toString(36), e.setAttribute("id", i));
							var n = e.getBoundingClientRect();
							r[i] = n
						}
					}), i.forEach(function (e) {
						if (e) {
							var i = e.getAttribute("id"),
								n = e.getAttribute("data-fx-id"),
								o = t.element.querySelector("#" + n),
								s = r[i],
								a = r[n],
								l = e.textContent.trim().length > 0,
								c = 1;
							void 0 !== a && (r[i].dx = s.left - a.left, r[i].dy = s.top - a.top, c = l ? parseInt(window.getComputedStyle(o)["font-size"], 10) / parseInt(window.getComputedStyle(e)["font-size"], 10) : a.height / s.height, r[i].scale = c)
						}
					}), this._fxCondenses = {
						elements: e,
						targets: i,
						bounds: r
					}
				},
				run: function (t, e) {
					var i = this,
						n = this._fxCondenses;
					this.condenses || (e = 0), t >= 1 ? n.elements.forEach(function (t) {
						t && (t.style.willChange = "opacity", t.style.opacity = -1 !== n.targets.indexOf(t) ? 0 : 1)
					}) : n.elements.forEach(function (t) {
						t && (t.style.willChange = "opacity", t.style.opacity = -1 !== n.targets.indexOf(t) ? 1 : 0)
					}), n.targets.forEach(function (r) {
						if (r) {
							var s = r.getAttribute("id");
							o(Math.min(1, t), [
								[1, n.bounds[s].scale],
								[0, -n.bounds[s].dx],
								[e, e - n.bounds[s].dy]
							], function (t, e, n) {
								r.style.willChange = "transform", e = e.toFixed(5), n = n.toFixed(5), t = t.toFixed(5), i._transform("translate(" + e + "px, " + n + "px) scale3d(" + t + ", " + t + ", 1)", r)
							})
						}
					})
				},
				tearDown: function () {
					delete this._fxCondenses
				}
			};
		i.d(e, "a", function () {
			return a
		}), i.d(e, !1, function () {
			return r
		}), i.d(e, !1, function () {
			return s
		});
		var a = [r, s]
	}, function (t, e, i) {
		"use strict";
		var n = i(2),
			r = function (t) {
				var e = {
					query: t,
					queryMatches: null,
					_reset: function () {
						this._removeListener(), this.queryMatches = null, this.query && (this._mq = window.matchMedia(this.query), this._addListener(), this._handler(this._mq))
					},
					_handler: function (t) {
						this.queryMatches = t.matches
					},
					_addListener: function () {
						this._mq && this._mq.addListener(this._handler)
					},
					_removeListener: function () {
						this._mq && this._mq.removeListener(this._handler), this._mq = null
					},
					init: function () {
						Object(n.watch)(this, "query", this._reset), this._reset()
					},
					destroy: function () {
						Object(n.unwatch)(this, "query", this._reset), this._removeListener()
					}
				};
				return e._reset = e._reset.bind(e), e._handler = e._handler.bind(e), e.init(), e
			};
		i.d(e, "a", function () {
			return r
		})
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			r = i(3),
			o = i(0),
			s = i(8),
			a = i(4),
			l = i(5),
			c = ".mdk-header__bg",
			h = c + "-front",
			u = c + "-rear",
			f = function (t) {
				return {
					properties: {
						condenses: {
							type: Boolean,
							reflectToAttribute: !0
						},
						reveals: {
							type: Boolean,
							reflectToAttribute: !0
						},
						fixed: {
							type: Boolean,
							reflectToAttribute: !0
						},
						disabled: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					observers: ["_handleFixedPositionedScroll(scrollTarget)", "_reset(condenses, reveals, fixed)"],
					listeners: ["window._debounceResize(resize)"],
					mixins: [Object(n.a)(t), Object(r.a)(t)],
					_height: 0,
					_dHeight: 0,
					_primaryElementTop: 0,
					_primaryElement: null,
					_top: 0,
					_progress: 0,
					_wasScrollingDown: !1,
					_initScrollTop: 0,
					_initTimestamp: 0,
					_lastTimestamp: 0,
					_lastScrollTop: 0,
					get transformDisabled() {
						return this.disabled || this.element.dataset.transformDisabled || !this._isPositionedFixedEmulated
					},
					set transformDisabled(t) {
						this.element[t ? "setAttribute" : "removeAttribute"]("data-transform-disabled", "data-transform-disabled")
					},
					get _maxHeaderTop() {
						return this.fixed ? this._dHeight : this._height + 5
					},
					get _isPositionedFixedEmulated() {
						return this.fixed || this.condenses || this.reveals
					},
					get _isPositionedAbsolute() {
						return "absolute" === window.getComputedStyle(this.element).position
					},
					willCondense: function () {
						return this._dHeight > 0 && this.condenses
					},
					isOnScreen: function () {
						return 0 !== this._height && this._top < this._height
					},
					isContentBelow: function () {
						return 0 === this._top ? this._clampedScrollTop > 0 : this._clampedScrollTop - this._maxHeaderTop >= 0
					},
					getScrollState: function () {
						return {
							progress: this._progress,
							top: this._top
						}
					},
					_setupBackgrounds: function () {
						var t = this.element.querySelector(c);
						t || (t = document.createElement("DIV"), this.element.insertBefore(t, this.element.childNodes[0]), t.classList.add(c.substr(1))), [h, u].map(function (e) {
							var i = t.querySelector(e);
							i || (i = document.createElement("DIV"), t.appendChild(i), i.classList.add(e.substr(1)))
						})
					},
					_reset: function () {
						if (0 !== this.element.offsetWidth || 0 !== this.element.offsetHeight) {
							this.element.classList[this._isPositionedFixedEmulated ? "add" : "remove"]("mdk-header--fixed"), this._transform("translate3d(0, 0, 0)"), this._primaryElement && this._transform("translate3d(0, 0, 0)", this._primaryElement);
							var t = this._clampedScrollTop,
								e = 0 === this._height || 0 === t;
							this._height = this.element.offsetHeight, this._primaryElement = this._getPrimaryElement(), this._primaryElementTop = this._primaryElement ? this._primaryElement.offsetTop : 0, this._dHeight = 0, this._mayMove() && (this._dHeight = this._primaryElement ? this._height - this._primaryElement.offsetHeight : 0), this._setUpEffects(), this._updateScrollState(e ? t : this._lastScrollTop, !0)
						}
					},
					_handleFixedPositionedScroll: function () {
						void 0 !== this._fixedPositionedScrollHandler && this._fixedPositionedScrollHandler.restore(), this._isValidScrollTarget() && this._isPositionedFixedEmulated && this.scrollTarget !== this._doc && (this._fixedPositionedScrollHandler = Object(s.RetargetMouseScroll)(this.element, this.scrollTarget))
					},
					_getPrimaryElement: function () {
						for (var t = void 0, e = this.element.querySelector(".mdk-header__content").children, i = 0; i < e.length; i++)
							if (e[i].nodeType === Node.ELEMENT_NODE) {
								var n = e[i];
								if (n.dataset.primary) {
									t = n;
									break
								}
								t || (t = n)
							}
						return t
					},
					_updateScrollState: function (t, e) {
						var i = this;
						if (0 !== this._height && !this.disabled) {
							var n = 0,
								r = 0,
								o = this._top,
								s = this._lastScrollTop,
								a = this._maxHeaderTop,
								l = t - s,
								c = Math.abs(l),
								h = t > s,
								u = Date.now();
							if (e || t !== s) {
								if (this._mayMove() && (r = this._clamp(this.reveals ? o + l : t, 0, a)), t >= this._dHeight && (r = this.condenses ? Math.max(this._dHeight, r) : r, this.element.style.transitionDuration = "0ms"), this.reveals && c < 100 && ((u - this._initTimestamp > 300 || this._wasScrollingDown !== h) && (this._initScrollTop = t, this._initTimestamp = u), t >= a))
									if (Math.abs(this._initScrollTop - t) > 30 || c > 10) {
										h && t >= a ? r = a : !h && t >= this._dHeight && (r = this.condenses ? this._dHeight : 0);
										var f = l / (u - this._lastTimestamp);
										this.element.style.transitionDuration = this._clamp((r - o) / f, 0, 300) + "ms"
									} else r = this._top;
								n = 0 === this._dHeight ? t > 0 ? 1 : 0 : r / this._dHeight, e || (this._lastScrollTop = t, this._top = r, this._wasScrollingDown = h, this._lastTimestamp = u), (e || n !== this._progress || o !== r || 0 === t) && (this._progress = n, requestAnimationFrame(function () {
									i._runEffects(n, r), i._transformHeader(r)
								}))
							}
						}
					},
					_transformHeader: function (t) {
						if (!this.transformDisabled) {
							var e = t;
							this._isPositionedAbsolute && this.scrollTarget === this._doc && (e = 0), t === e && (this.element.style.willChange = "transform", this._transform("translate3d(0, " + -1 * e + "px, 0)")), this._primaryElement && this.condenses && t >= this._primaryElementTop && (this._primaryElement.style.willChange = "transform", this._transform("translate3d(0, " + (Math.min(t, this._dHeight) - this._primaryElementTop) + "px, 0)", this._primaryElement))
						}
					},
					_clamp: function (t, e, i) {
						return Math.min(i, Math.max(e, t))
					},
					_mayMove: function () {
						return this.condenses || !this.fixed
					},
					_debounceResize: function () {
						var t = this;
						clearTimeout(this._onResizeTimeout), this._resizeWidth !== window.innerWidth && (this._onResizeTimeout = setTimeout(function () {
							t._resizeWidth = window.innerWidth, t._reset()
						}, 50))
					},
					init: function () {
						var t = this;
						this._resizeWidth = window.innerWidth, this.attachToScrollTarget(), this._handleFixedPositionedScroll(), this._setupBackgrounds(), a.a.concat(l.a).map(function (e) {
							return t.registerEffect(e.name, e)
						})
					},
					destroy: function () {
						clearTimeout(this._onResizeTimeout), this.detachFromScrollTarget()
					}
				}
			};
		o.handler.register("mdk-header", f), i.d(e, "headerComponent", function () {
			return f
		})
	}, function (t, e) {
		"function" != typeof this.RetargetMouseScroll && function () {
			function t(t, e, i, n) {
				i && (t.preventDefault ? t.preventDefault() : event.returnValue = !1);
				var r = t.detail || -t.wheelDelta / 40;
				r *= 19, "number" != typeof n || isNaN(n) || (r *= n), t.wheelDeltaX || "axis" in t && "HORIZONTAL_AXIS" in t && t.axis == t.HORIZONTAL_AXIS ? e.scrollBy ? e.scrollBy(r, 0) : e.scrollLeft += r : e.scrollBy ? e.scrollBy(0, r) : e.scrollTop += r
			}
			var e = ["DOMMouseScroll", "mousewheel"];
			this.RetargetMouseScroll = function (i, n, r, o, s) {
				i || (i = document), n || (n = window), "boolean" != typeof r && (r = !0), "function" != typeof s && (s = null);
				var a, l, c, h = function (e) {
					e = e || window.event, s && s.call(this, e) || t(e, n, r, o)
				};
				return (a = i.addEventListener) ? (a.call(i, e[0], h, !1), a.call(i, e[1], h, !1)) : (a = i.attachEvent) && a.call(i, "on" + e[1], h), (l = i.removeEventListener) ? c = function () {
					l.call(i, e[0], h, !1), l.call(i, e[1], h, !1)
				} : (l = i.detachEvent) && (c = function () {
					l.call(i, "on" + e[1], h)
				}), {
						restore: c
					}
			}
		}.call(this)
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			if (Array.isArray(t)) {
				for (var e = 0, i = Array(t.length); e < t.length; e++) i[e] = t[e];
				return i
			}
			return Array.from(t)
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(0),
			o = function () {
				return {
					properties: {
						hasScrollingRegion: {
							type: Boolean,
							reflectToAttribute: !0
						},
						fullbleed: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					observers: ["_updateScroller(hasScrollingRegion)", "_updateContentPosition(hasScrollingRegion, header.fixed, header.condenses)", "_updateDocument(fullbleed)"],
					listeners: ["window._debounceResize(resize)"],
					get contentContainer() {
						return this.element.querySelector(".mdk-header-layout__content")
					},
					get header() {
						var t = this.element.querySelector(".mdk-header");
						if (t) return t.mdkHeader
					},
					_updateScroller: function () {
						this.header.scrollTargetSelector = this.hasScrollingRegion ? this.contentContainer : null
					},
					_updateContentPosition: function () {
						var t = this.header.element.offsetHeight,
							e = parseInt(window.getComputedStyle(this.header.element).marginBottom, 10),
							i = this.contentContainer.style;
						this.header.fixed && !this.header.willCondense() && this.hasScrollingRegion ? (i.marginTop = t + "px", i.paddingTop = e + "px") : (i.paddingTop = t + e + "px", i.marginTop = "")
					},
					_debounceResize: function () {
						var t = this;
						clearTimeout(this._onResizeTimeout), this._resizeWidth !== window.innerWidth && (this._onResizeTimeout = setTimeout(function () {
							t._resizeWidth = window.innerWidth, t._reset()
						}, 50))
					},
					_updateDocument: function () {
						var t = [].concat(n(document.querySelectorAll("html, body")));
						this.fullbleed && t.forEach(function (t) {
							t.style.height = "100%"
						})
					},
					_reset: function () {
						this._updateContentPosition()
					},
					init: function () {
						this._resizeWidth = window.innerWidth, this._updateDocument(), this._updateScroller()
					},
					destroy: function () {
						clearTimeout(this._onResizeTimeout)
					}
				}
			};
		r.handler.register("mdk-header-layout", o), i.d(e, "headerLayoutComponent", function () {
			return o
		})
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			r = i(3),
			o = i(0),
			s = i(4),
			a = ".mdk-box__bg",
			l = a + "-front",
			c = a + "-rear",
			h = function (t) {
				return {
					properties: {
						disabled: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					listeners: ["window._debounceResize(resize)"],
					mixins: [Object(n.a)(t), Object(r.a)(t)],
					_progress: 0,
					isOnScreen: function () {
						return this._elementTop < this._scrollTop + this._scrollTargetHeight && this._elementTop + this.element.offsetHeight > this._scrollTop
					},
					isVisible: function () {
						return this.element.offsetWidth > 0 && this.element.offsetHeight > 0
					},
					getScrollState: function () {
						return {
							progress: this._progress
						}
					},
					_setupBackgrounds: function () {
						var t = this.element.querySelector(a);
						t || (t = document.createElement("DIV"), this.element.insertBefore(t, this.element.childNodes[0]), t.classList.add(a.substr(1))), [l, c].map(function (e) {
							var i = t.querySelector(e);
							i || (i = document.createElement("DIV"), t.appendChild(i), i.classList.add(e.substr(1)))
						})
					},
					_getElementTop: function () {
						for (var t = this.element, e = 0; t && t !== this.scrollTarget;) e += t.offsetTop, t = t.offsetParent;
						return e
					},
					_updateScrollState: function (t) {
						if (!this.disabled && this.isOnScreen()) {
							var e = Math.min(this._scrollTargetHeight, this._elementTop + this.element.offsetHeight),
								i = this._elementTop - t,
								n = 1 - (i + this.element.offsetHeight) / e;
							this._progress = n, this._runEffects(this._progress, t)
						}
					},
					_debounceResize: function () {
						var t = this;
						clearTimeout(this._onResizeTimeout), this._resizeWidth !== window.innerWidth && (this._onResizeTimeout = setTimeout(function () {
							t._resizeWidth = window.innerWidth, t._reset()
						}, 50))
					},
					init: function () {
						var t = this;
						this._resizeWidth = window.innerWidth, this.attachToScrollTarget(), this._setupBackgrounds(), s.a.map(function (e) {
							return t.registerEffect(e.name, e)
						})
					},
					_reset: function () {
						this._elementTop = this._getElementTop(), this._setUpEffects(), this._updateScrollState(this._clampedScrollTop)
					},
					destroy: function () {
						clearTimeout(this._onResizeTimeout), this.detachFromScrollTarget()
					}
				}
			};
		o.handler.register("mdk-box", h), i.d(e, "boxComponent", function () {
			return h
		})
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			r = function () {
				return {
					properties: {
						opened: {
							type: Boolean,
							reflectToAttribute: !0
						},
						persistent: {
							type: Boolean,
							reflectToAttribute: !0
						},
						align: {
							reflectToAttribute: !0,
							value: "start"
						},
						position: {
							reflectToAttribute: !0
						}
					},
					observers: ["_resetPosition(align)", "_fireChange(opened, persistent, align, position)", "_onChangedState(_drawerState)", "_onClose(opened)"],
					listeners: ["_onTransitionend(transitionend)", "scrim._onClickScrim(click)"],
					_drawerState: 0,
					_DRAWER_STATE: {
						INIT: 0,
						OPENED: 1,
						OPENED_PERSISTENT: 2,
						CLOSED: 3
					},
					get contentContainer() {
						return this.element.querySelector(".mdk-drawer__content")
					},
					get scrim() {
						var t = this.element.querySelector(".mdk-drawer__scrim");
						return t || (t = document.createElement("DIV"), this.element.insertBefore(t, this.element.childNodes[0]), t.classList.add("mdk-drawer__scrim")), t
					},
					getWidth: function () {
						return this.contentContainer.offsetWidth
					},
					toggle: function () {
						this.opened = !this.opened
					},
					close: function () {
						this.opened = !1
					},
					open: function () {
						this.opened = !0
					},
					_onClose: function (t) {
						t || this.element.setAttribute("data-closing", !0)
					},
					_isRTL: function () {
						return "rtl" === window.getComputedStyle(this.element).direction
					},
					_setTransitionDuration: function (t) {
						this.contentContainer.style.transitionDuration = t, this.scrim.style.transitionDuration = t
					},
					_resetDrawerState: function () {
						var t = this._drawerState;
						this.opened ? this._drawerState = this.persistent ? this._DRAWER_STATE.OPENED_PERSISTENT : this._DRAWER_STATE.OPENED : this._drawerState = this._DRAWER_STATE.CLOSED, t !== this._drawerState && (this.opened || this.element.removeAttribute("data-closing"), this._drawerState === this._DRAWER_STATE.OPENED ? document.body.style.overflow = "hidden" : document.body.style.overflow = "")
					},
					_resetPosition: function () {
						switch (this.align) {
							case "start":
								return void (this.position = this._isRTL() ? "right" : "left");
							case "end":
								return void (this.position = this._isRTL() ? "left" : "right")
						}
						this.position = this.align
					},
					_fireChange: function () {
						this.fire("mdk-drawer-change")
					},
					_fireChanged: function () {
						this.fire("mdk-drawer-changed")
					},
					_onTransitionend: function (t) {
						var e = t.target;
						e !== this.contentContainer && e !== this.scrim || this._resetDrawerState()
					},
					_onClickScrim: function (t) {
						t.preventDefault(), this.close()
					},
					_onChangedState: function (t, e) {
						e !== this._DRAWER_STATE.INIT && this._fireChanged()
					},
					init: function () {
						var t = this;
						this._resetPosition(), this._setTransitionDuration("0s"), setTimeout(function () {
							t._setTransitionDuration(""), t._resetDrawerState()
						}, 0)
					}
				}
			};
		n.handler.register("mdk-drawer", r), i.d(e, "drawerComponent", function () {
			return r
		})
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			if (Array.isArray(t)) {
				for (var e = 0, i = Array(t.length); e < t.length; e++) i[e] = t[e];
				return i
			}
			return Array.from(t)
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(6),
			o = i(0),
			s = function () {
				return {
					properties: {
						forceNarrow: {
							type: Boolean,
							reflectToAttribute: !0
						},
						push: {
							type: Boolean,
							reflectToAttribute: !0
						},
						responsiveWidth: {
							reflectToAttribute: !0,
							value: "554px"
						},
						hasScrollingRegion: {
							type: Boolean,
							reflectToAttribute: !0
						},
						fullbleed: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					observers: ["_resetLayout(narrow, forceNarrow)", "_onQueryMatches(mediaQuery.queryMatches)", "_updateScroller(hasScrollingRegion)", "_updateDocument(fullbleed)"],
					listeners: ["drawer._onDrawerChange(mdk-drawer-change)"],
					_narrow: null,
					_mediaQuery: null,
					get mediaQuery() {
						return this._mediaQuery || (this._mediaQuery = Object(r.a)(this.responsiveMediaQuery)), this._mediaQuery
					},
					get narrow() {
						return !!this.forceNarrow || this._narrow
					},
					set narrow(t) {
						this._narrow = !(t || !this.forceNarrow) || t
					},
					get contentContainer() {
						return this.element.querySelector(".mdk-drawer-layout__content")
					},
					get drawer() {
						var t = this.element.querySelector(".mdk-drawer");
						if (t) return t.mdkDrawer
					},
					get responsiveMediaQuery() {
						return this.forceNarrow ? "(min-width: 0px)" : "(max-width: " + this.responsiveWidth + ")"
					},
					_updateDocument: function () {
						var t = [].concat(n(document.querySelectorAll("html, body")));
						this.fullbleed && t.forEach(function (t) {
							//t.style.height = "100%"
						})
					},
					_updateScroller: function () {
						var t = [].concat(n(document.querySelectorAll("html, body")));
						this.hasScrollingRegion && t.forEach(function (t) {
							//t.style.overflow = "hidden", t.style.position = "relative"
						})
					},
					_resetLayout: function () {
						this.drawer.opened = this.drawer.persistent = !this.narrow, this._onDrawerChange();
						var t = this.element.querySelector(".mdk-drawer-layout");
						t && (t.style.paddingBottom = t.offsetTop + "px")
					},
					_resetContent: function () {
						var t = this.drawer,
							e = this.drawer.getWidth(),
							i = this.contentContainer,
							n = t._isRTL();
						if (!t.opened) return i.style.marginLeft = "", void (i.style.marginRight = "");
						"right" === t.position || !t.position && n ? (i.style.marginLeft = "", i.style.marginRight = e + "px") : (i.style.marginLeft = e + "px", i.style.marginRight = "")
					},
					_resetPush: function () {
						var t = this.drawer,
							e = this.drawer.getWidth(),
							i = this.contentContainer,
							n = t._isRTL();
						if (!t.opened) return o.util.transform("translate3d(0, 0, 0)", i), i.style.marginLeft = "", void (i.style.marginRight = "");
						"right" === t.position || !t.position && n ? (o.util.transform("translate3d(" + -1 * e + "px, 0, 0)", i), this.narrow || (i.style.marginLeft = e + "px", i.style.marginRight = "")) : (o.util.transform("translate3d(" + e + "px, 0, 0)", i), this.narrow || (i.style.marginLeft = "", i.style.marginRight = e + "px"))
					},
					_setContentTransitionDuration: function (t) {
						this.contentContainer.style.transitionDuration = t
					},
					_onDrawerChange: function () {
						if (this.push) return this._resetPush();
						this._resetContent()
					},
					_onQueryMatches: function (t) {
						this.narrow = t
					},
					init: function () {
						var t = this;
						this._setContentTransitionDuration("0s"), setTimeout(function () {
							return t._setContentTransitionDuration("")
						}, 0), this._updateDocument(), this._updateScroller(), this.mediaQuery.init()
					},
					destroy: function () {
						this.mediaQuery.destroy()
					}
				}
			};
		o.handler.register("mdk-drawer-layout", s), i.d(e, "drawerLayoutComponent", function () {
			return s
		})
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			r = function () {
				return {
					properties: {
						partialHeight: {
							reflectToAttribute: !0,
							value: 0
						},
						forceReveal: {
							type: Boolean,
							reflectToAttribute: !0
						},
						hoverReveal: {
							type: Boolean,
							reflectToAttribute: !0
						},
						opened: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					observers: ["_onChange(opened)"],
					listeners: ["_onEnter(mouseenter, touchstart)", "_onLeave(mouseleave, touchend)", "window._debounceResize(resize)"],
					get reveal() {
						return this.element.querySelector(".mdk-reveal__content")
					},
					get partial() {
						var t = this.reveal.querySelector(".mdk-reveal__partial");
						return t || (t = document.createElement("DIV"), t.classList.add("mdk-reveal__partial"), this.reveal.insertBefore(t, this.reveal.childNodes[0])), t
					},
					open: function () {
						this.opened = !0
					},
					close: function () {
						this.opened = !1
					},
					toggle: function () {
						this.opened = !this.opened
					},
					_reset: function () {
						var t = this,
							e = parseInt(window.getComputedStyle(this.reveal)["margin-top"], 10),
							i = this.reveal.offsetHeight,
							r = "translate3d(0, " + (i - this.partialHeight) + "px, 0)";
						this._translateReveal = r, this.forceReveal && (r = "translate3d(0, 0, 0)"), 0 !== this.partialHeight && (this.partial.style.height = this.partialHeight + "px"), this.reveal.style.transitionDuration = "0s", n.util.transform(r, this.reveal), this.element.style.height = e + i + "px", setTimeout(function () {
							return t.reveal.style.transitionDuration = ""
						}, 0)
					},
					_onChange: function () {
						n.util.transform(this.opened || this.forceReveal ? "translate3d(0, 0, 0)" : this._translateReveal, this.reveal)
					},
					_onEnter: function () {
						this.hoverReveal && !this.forceReveal && this.open()
					},
					_onLeave: function () {
						this.hoverReveal && !this.forceReveal && this.close()
					},
					_debounceResize: function () {
						var t = this;
						clearTimeout(this._debounceResizeTimer), this._debounceResizeTimer = setTimeout(function () {
							t._resizeWidth !== window.innerWidth && (t._resizeWidth = window.innerWidth, t._reset())
						}, 50)
					},
					init: function () {
						this._resizeWidth = window.innerWidth
					},
					destroy: function () {
						clearTimeout(this._debounceResizeTimer)
					}
				}
			};
		n.handler.register("mdk-reveal", r), i.d(e, "revealComponent", function () {
			return r
		})
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			if (Array.isArray(t)) {
				for (var e = 0, i = Array(t.length); e < t.length; e++) i[e] = t[e];
				return i
			}
			return Array.from(t)
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(0),
			o = function () {
				return "ontouchstart" in window
			},
			s = function (t) {
				"none" === t && (t = "matrix(0,0,0,0,0)");
				var e = {},
					i = t.match(/([-+]?[\d\.]+)/g);
				return e.translate = {
					x: parseInt(i[4], 10) || 0,
					y: parseInt(i[5], 10) || 0
				}, e
			},
			a = function (t) {
				var e = window.getComputedStyle(t, null),
					i = e.getPropertyValue("-webkit-transform") || e.getPropertyValue("-moz-transform") || e.getPropertyValue("-ms-transform") || e.getPropertyValue("-o-transform") || e.getPropertyValue("transform");
				return s(i)
			},
			l = function (t) {
				return t = t.originalEvent || t || window.event, t = t.touches && t.touches.length ? t.touches[0] : t.changedTouches && t.changedTouches.length ? t.changedTouches[0] : t, {
					x: t.pageX ? t.pageX : t.clientX,
					y: t.pageY ? t.pageY : t.clientY
				}
			},
			c = function (t, e) {
				return {
					x: t.x - e.x,
					y: t.y - e.y
				}
			},
			h = function () {
				return {
					listeners: ["_onEnter(mouseenter)", "_onLeave(mouseleave)", "_onTransitionend(transitionend)", "_onDragStart(mousedown, touchstart)", "_onMouseDrag(dragstart, selectstart)", "document._onDragMove(mousemove, touchmove)", "document._onDragEnd(mouseup, touchend)", "window._debounceResize(resize)"],
					_items: [],
					_isMoving: !1,
					_content: null,
					_current: null,
					_drag: {},
					_reset: function () {
						this._content = this.element.querySelector(".mdk-carousel__content"), this._items = [].concat(n(this._content.children)), this._content.style.width = "", this._items.forEach(function (t) {
							t.style.width = ""
						});
						var t = this.element.offsetWidth,
							e = this._items[0].offsetWidth,
							i = t / e;
						if (this._itemWidth = e, this._visible = Math.round(i), this._max = this._items.length - this._visible, this.element.style.overflow = "hidden", this._content.style.width = e * this._items.length + "px", this._items.forEach(function (t) {
							t.classList.add("mdk-carousel__item"), t.style.width = e + "px"
						}), this._current || (this._current = this._items[0]), !(this._items.length < 2)) {
							var r = this._items.indexOf(this._current);
							this._transform(r * e * -1, 0), this.start()
						}
					},
					start: function () {
						this.stop(), this._items.length < 2 || this._items.length <= this._visible || (this._setContentTransitionDuration(""), this._interval = setInterval(this.next.bind(this), 2e3))
					},
					stop: function () {
						clearInterval(this._interval), this._interval = null
					},
					next: function () {
						if (!(this._items.length < 2 || this._isMoving || document.hidden) && this._isOnScreen()) {
							var t = this._items.indexOf(this._current),
								e = void 0 !== this._items[t + 1] ? t + 1 : 0;
							this._items.length - t === this._visible && (e = 0), this._to(e)
						}
					},
					prev: function () {
						if (!(this._items.length < 2 || this._isMoving)) {
							var t = this._items.indexOf(this._current),
								e = void 0 !== this._items[t - 1] ? t - 1 : this._items.length;
							this._to(e)
						}
					},
					_transform: function (t, e, i) {
						void 0 !== e && this._setContentTransitionDuration(e + "ms"), a(this._content).translate.x === t ? "function" == typeof i && i.call(this) : requestAnimationFrame(function () {
							0 !== e && (this._isMoving = !0), r.util.transform("translate3d(" + t + "px, 0, 0)", this._content), "function" == typeof i && i.call(this)
						}.bind(this))
					},
					_to: function (t) {
						if (!(this._items.length < 2 || this._isMoving)) {
							t > this._max && (t = this._max), t < 0 && (t = 0);
							var e = t * this._itemWidth * -1;
							this._transform(e, !1, function () {
								this._current = this._items[t]
							})
						}
					},
					_debounceResize: function () {
						clearTimeout(this._resizeTimer), this._resizeWidth !== window.innerWidth && (this._resizeTimer = setTimeout(function () {
							this._resizeWidth = window.innerWidth, this.stop(), this._reset()
						}.bind(this), 50))
					},
					_setContentTransitionDuration: function (t) {
						this._content.style.transitionDuration = t
					},
					_onEnter: function () {
						this.stop()
					},
					_onLeave: function () {
						this._drag.wasDragging || this.start()
					},
					_onTransitionend: function () {
						this._isMoving = !1
					},
					_onDragStart: function (t) {
						if (!this._drag.isDragging && !this._isMoving && 3 !== t.which) {
							this.stop();
							var e = a(this._content).translate;
							this._drag.isDragging = !0, this._drag.isScrolling = !1, this._drag.time = (new Date).getTime(), this._drag.start = e, this._drag.current = e, this._drag.delta = {
								x: 0,
								y: 0
							}, this._drag.pointer = l(t), this._drag.target = t.target
						}
					},
					_onDragMove: function (t) {
						if (this._drag.isDragging) {
							var e = c(this._drag.pointer, l(t)),
								i = c(this._drag.start, e),
								n = o() && Math.abs(e.x) < Math.abs(e.y);
							n || (t.preventDefault(), this._transform(i.x, 0)), this._drag.delta = e, this._drag.current = i, this._drag.isScrolling = n, this._drag.target = t.target
						}
					},
					_onDragEnd: function (t) {
						if (this._drag.isDragging) {
							this._setContentTransitionDuration(""), this._drag.duration = (new Date).getTime() - this._drag.time;
							var e = Math.abs(this._drag.delta.x),
								i = e > 20 || e > this._itemWidth / 3,
								n = Math.max(Math.round(e / this._itemWidth), 1),
								r = this._drag.delta.x > 0;
							if (i) {
								var o = this._items.indexOf(this._current),
									s = r ? o + n : o - n;
								this._to(s)
							} else this._transform(this._drag.start.x);
							this._drag.isDragging = !1, this._drag.wasDragging = !0
						}
					},
					_onMouseDrag: function (t) {
						t.preventDefault(), t.stopPropagation()
					},
					_isOnScreen: function () {
						var t = this.element.getBoundingClientRect();
						return t.top >= 0 && t.left >= 0 && t.bottom <= window.innerHeight && t.right <= window.innerWidth
					},
					init: function () {
						this._resizeWidth = window.innerWidth, this._reset()
					},
					destroy: function () {
						this.stop(), clearTimeout(this._resizeTimer)
					}
				}
			};
		r.handler.register("mdk-carousel", h), i.d(e, "carouselComponent", function () {
			return h
		})
	}, , , , , , , , , , , function (t, e, i) {
		t.exports = i(26)
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			r = i(3),
			o = i(7),
			s = i(9),
			a = i(10),
			l = i(11),
			c = i(12),
			h = i(13),
			u = i(14),
			f = i(0),
			d = function (t) {
				return {
					properties: {
						for: {
							readOnly: !0,
							value: function () {
								var t = this.element.getAttribute("data-for");
								return document.querySelector("#" + t)
							}
						},
						position: {
							reflectToAttribute: !0,
							value: "bottom"
						},
						opened: {
							type: Boolean,
							reflectToAttribute: !0
						}
					},
					listeners: ["for.show(mouseenter, touchstart)", "for.hide(mouseleave, touchend)", "window._debounceResize(resize)"],
					observers: ["_reset(position)"],
					mixins: [Object(n.a)(t)],
					get drawerLayout() {
						var t = document.querySelector(".mdk-js-drawer-layout");
						if (t) return t.mdkDrawerLayout
					},
					_reset: function () {
						this.element.removeAttribute("style");
						var t = this.for.getBoundingClientRect(),
							e = t.left + t.width / 2,
							i = t.top + t.height / 2,
							n = this.element.offsetWidth / 2 * -1,
							r = this.element.offsetHeight / 2 * -1;
						"left" === this.position || "right" === this.position ? i + r < 0 ? (this.element.style.top = "0", this.element.style.marginTop = "0") : (this.element.style.top = i + "px", this.element.style.marginTop = r + "px") : e + n < 0 ? (this.element.style.left = "0", this.element.style.marginLeft = "0") : (this.element.style.left = e + "px", this.element.style.marginLeft = n + "px"), "top" === this.position ? this.element.style.top = t.top - this.element.offsetHeight - 10 + "px" : "right" === this.position ? this.element.style.left = t.left + t.width + 10 + "px" : "left" === this.position ? this.element.style.left = t.left - this.element.offsetWidth - 10 + "px" : this.element.style.top = t.top + t.height + 10 + "px"
					},
					_debounceResize: function () {
						var t = this;
						clearTimeout(this._debounceResizeTimer), this._debounceResizeTimer = setTimeout(function () {
							window.innerWidth !== t._debounceResizeWidth && (t._debounceResizeWidth = window.innerWidth, t._reset())
						}, 50)
					},
					_scrollHandler: function () {
						clearTimeout(this._debounceScrollTimer), this._debounceScrollTimer = setTimeout(this._reset.bind(this), 50)
					},
					show: function () {
						this.opened = !0
					},
					hide: function () {
						this.opened = !1
					},
					toggle: function () {
						this.opened = !this.opened
					},
					init: function () {
						document.body.appendChild(this.element), this._debounceResizeWidth = window.innerWidth, this.attachToScrollTarget(), this._reset(), this.drawerLayout && this.drawerLayout.hasScrollingRegion && (this.scrollTargetSelector = this.drawerLayout.contentContainer)
					},
					destroy: function () {
						clearTimeout(this._debounceResizeTimer), clearTimeout(this._debounceScrollTimer), this.detachFromScrollTarget()
					}
				}
			};
		f.handler.register("mdk-tooltip", d);
		var _ = i(4),
			m = i(5),
			g = i(6);
		i.d(e, "scrollTargetBehavior", function () {
			return n.a
		}), i.d(e, "scrollEffectBehavior", function () {
			return r.a
		}), i.d(e, "headerComponent", function () {
			return o.headerComponent
		}), i.d(e, "headerLayoutComponent", function () {
			return s.headerLayoutComponent
		}), i.d(e, "boxComponent", function () {
			return a.boxComponent
		}), i.d(e, "drawerComponent", function () {
			return l.drawerComponent
		}), i.d(e, "drawerLayoutComponent", function () {
			return c.drawerLayoutComponent
		}), i.d(e, "revealComponent", function () {
			return h.revealComponent
		}), i.d(e, "carouselComponent", function () {
			return u.carouselComponent
		}), i.d(e, "tooltipComponent", function () {
			return d
		}), i.d(e, "SCROLL_EFFECTS", function () {
			return _.a
		}), i.d(e, "HEADER_SCROLL_EFFECTS", function () {
			return m.a
		}), i.d(e, "mediaQuery", function () {
			return g.a
		})
	}])
});