define(['exports', 'module', 'react'], function (exports, module, _react) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

  module.exports = createContextWrapper;

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  function _objectWithoutProperties(obj, keys) { var target = {}; for (var i in obj) { if (keys.indexOf(i) >= 0) continue; if (!Object.prototype.hasOwnProperty.call(obj, i)) continue; target[i] = obj[i]; } return target; }

  function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

  function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) subClass.__proto__ = superClass; }

  var _React = _interopRequireDefault(_react);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  /**
   * Creates new trigger class that injects context into overlay.
   */

  function createContextWrapper(Trigger, propName) {
    return function (contextTypes) {
      var ContextWrapper = (function (_React$Component) {
        function ContextWrapper() {
          _classCallCheck(this, ContextWrapper);

          if (_React$Component != null) {
            _React$Component.apply(this, arguments);
          }
        }

        _inherits(ContextWrapper, _React$Component);

        _createClass(ContextWrapper, [{
          key: 'getChildContext',
          value: function getChildContext() {
            return this.props.context;
          }
        }, {
          key: 'render',
          value: function render() {
            // Strip injected props from below.
            var _props = this.props;
            var wrapped = _props.wrapped;
            var context = _props.context;

            var props = _objectWithoutProperties(_props, ['wrapped', 'context']);

            return _React['default'].cloneElement(wrapped, props);
          }
        }]);

        return ContextWrapper;
      })(_React['default'].Component);

      ContextWrapper.childContextTypes = contextTypes;

      var TriggerWithContext = (function () {
        function TriggerWithContext() {
          _classCallCheck(this, TriggerWithContext);
        }

        _createClass(TriggerWithContext, [{
          key: 'render',
          value: function render() {
            var props = _extends({}, this.props);
            props[propName] = this.getWrappedOverlay();

            return _React['default'].createElement(
              Trigger,
              props,
              this.props.children
            );
          }
        }, {
          key: 'getWrappedOverlay',
          value: function getWrappedOverlay() {
            return _React['default'].createElement(ContextWrapper, {
              context: this.context,
              wrapped: this.props[propName]
            });
          }
        }]);

        return TriggerWithContext;
      })();

      TriggerWithContext.contextTypes = contextTypes;

      return TriggerWithContext;
    };
  }
});