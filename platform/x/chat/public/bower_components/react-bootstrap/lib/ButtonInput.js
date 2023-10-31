define(['exports', 'module', 'react', './Button', './FormGroup', './InputBase', './utils/childrenValueInputValidation'], function (exports, module, _react, _Button, _FormGroup, _InputBase2, _utilsChildrenValueInputValidation) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  function _objectWithoutProperties(obj, keys) { var target = {}; for (var i in obj) { if (keys.indexOf(i) >= 0) continue; if (!Object.prototype.hasOwnProperty.call(obj, i)) continue; target[i] = obj[i]; } return target; }

  function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

  function _inherits(subClass, superClass) { if (typeof superClass !== 'function' && superClass !== null) { throw new TypeError('Super expression must either be null or a function, not ' + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) subClass.__proto__ = superClass; }

  var _React = _interopRequireDefault(_react);

  var _Button2 = _interopRequireDefault(_Button);

  var _FormGroup2 = _interopRequireDefault(_FormGroup);

  var _InputBase3 = _interopRequireDefault(_InputBase2);

  var _childrenValueValidation = _interopRequireDefault(_utilsChildrenValueInputValidation);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var ButtonInput = (function (_InputBase) {
    function ButtonInput() {
      _classCallCheck(this, ButtonInput);

      if (_InputBase != null) {
        _InputBase.apply(this, arguments);
      }
    }

    _inherits(ButtonInput, _InputBase);

    _createClass(ButtonInput, [{
      key: 'renderFormGroup',
      value: function renderFormGroup(children) {
        var _props = this.props;
        var bsStyle = _props.bsStyle;
        var value = _props.value;

        var other = _objectWithoutProperties(_props, ['bsStyle', 'value']);

        return _React['default'].createElement(
          _FormGroup2['default'],
          other,
          children
        );
      }
    }, {
      key: 'renderInput',
      value: function renderInput() {
        var _props2 = this.props;
        var children = _props2.children;
        var value = _props2.value;

        var other = _objectWithoutProperties(_props2, ['children', 'value']);

        var val = children ? children : value;
        return _React['default'].createElement(_Button2['default'], _extends({}, other, { componentClass: 'input', ref: 'input', key: 'input', value: val }));
      }
    }]);

    return ButtonInput;
  })(_InputBase3['default']);

  ButtonInput.types = ['button', 'reset', 'submit'];

  ButtonInput.defaultProps = {
    type: 'button'
  };

  ButtonInput.propTypes = {
    type: _React['default'].PropTypes.oneOf(ButtonInput.types),
    bsStyle: function bsStyle(props) {
      //defer to Button propTypes of bsStyle
      return null;
    },
    children: _childrenValueValidation['default'],
    value: _childrenValueValidation['default']
  };

  module.exports = ButtonInput;
});