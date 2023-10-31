define(['exports', 'module', 'react', 'classnames', './BootstrapMixin', './utils/ValidComponentChildren'], function (exports, module, _react, _classnames, _BootstrapMixin, _utilsValidComponentChildren) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  /* eslint react/prop-types: [2, {ignore: "bsStyle"}] */
  /* BootstrapMixin contains `bsStyle` type validation */

  var _React = _interopRequireDefault(_react);

  var _classNames = _interopRequireDefault(_classnames);

  var _BootstrapMixin2 = _interopRequireDefault(_BootstrapMixin);

  var _ValidComponentChildren = _interopRequireDefault(_utilsValidComponentChildren);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var PanelGroup = _React['default'].createClass({
    displayName: 'PanelGroup',

    mixins: [_BootstrapMixin2['default']],

    propTypes: {
      accordion: _React['default'].PropTypes.bool,
      activeKey: _React['default'].PropTypes.any,
      className: _React['default'].PropTypes.string,
      children: _React['default'].PropTypes.node,
      defaultActiveKey: _React['default'].PropTypes.any,
      onSelect: _React['default'].PropTypes.func
    },

    getDefaultProps: function getDefaultProps() {
      return {
        bsClass: 'panel-group'
      };
    },

    getInitialState: function getInitialState() {
      var defaultActiveKey = this.props.defaultActiveKey;

      return {
        activeKey: defaultActiveKey
      };
    },

    render: function render() {
      var classes = this.getBsClassSet();
      return _React['default'].createElement(
        'div',
        _extends({}, this.props, { className: (0, _classNames['default'])(this.props.className, classes), onSelect: null }),
        _ValidComponentChildren['default'].map(this.props.children, this.renderPanel)
      );
    },

    renderPanel: function renderPanel(child, index) {
      var activeKey = this.props.activeKey != null ? this.props.activeKey : this.state.activeKey;

      var props = {
        bsStyle: child.props.bsStyle || this.props.bsStyle,
        key: child.key ? child.key : index,
        ref: child.ref
      };

      if (this.props.accordion) {
        props.collapsible = true;
        props.expanded = child.props.eventKey === activeKey;
        props.onSelect = this.handleSelect;
      }

      return (0, _react.cloneElement)(child, props);
    },

    shouldComponentUpdate: function shouldComponentUpdate() {
      // Defer any updates to this component during the `onSelect` handler.
      return !this._isChanging;
    },

    handleSelect: function handleSelect(e, key) {
      e.preventDefault();

      if (this.props.onSelect) {
        this._isChanging = true;
        this.props.onSelect(key);
        this._isChanging = false;
      }

      if (this.state.activeKey === key) {
        key = null;
      }

      this.setState({
        activeKey: key
      });
    }
  });

  module.exports = PanelGroup;
});