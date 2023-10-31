define(['exports', 'module', 'react', 'classnames', './utils/CustomPropTypes'], function (exports, module, _react, _classnames, _utilsCustomPropTypes) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  var _React = _interopRequireDefault(_react);

  var _classNames = _interopRequireDefault(_classnames);

  var _CustomPropTypes = _interopRequireDefault(_utilsCustomPropTypes);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var Grid = _React['default'].createClass({
    displayName: 'Grid',

    propTypes: {
      fluid: _React['default'].PropTypes.bool,
      componentClass: _CustomPropTypes['default'].elementType
    },

    getDefaultProps: function getDefaultProps() {
      return {
        componentClass: 'div'
      };
    },

    render: function render() {
      var ComponentClass = this.props.componentClass;
      var className = this.props.fluid ? 'container-fluid' : 'container';

      return _React['default'].createElement(
        ComponentClass,
        _extends({}, this.props, {
          className: (0, _classNames['default'])(this.props.className, className) }),
        this.props.children
      );
    }
  });

  module.exports = Grid;
});