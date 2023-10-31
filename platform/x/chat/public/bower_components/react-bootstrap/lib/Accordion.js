define(['exports', 'module', 'react', './PanelGroup'], function (exports, module, _react, _PanelGroup) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  var _React = _interopRequireDefault(_react);

  var _PanelGroup2 = _interopRequireDefault(_PanelGroup);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var Accordion = _React['default'].createClass({
    displayName: 'Accordion',

    render: function render() {
      return _React['default'].createElement(
        _PanelGroup2['default'],
        _extends({}, this.props, { accordion: true }),
        this.props.children
      );
    }
  });

  module.exports = Accordion;
});