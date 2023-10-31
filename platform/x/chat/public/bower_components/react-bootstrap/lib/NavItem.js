define(['exports', 'module', 'react', 'classnames', './BootstrapMixin'], function (exports, module, _react, _classnames, _BootstrapMixin) {
  'use strict';

  var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  function _objectWithoutProperties(obj, keys) { var target = {}; for (var i in obj) { if (keys.indexOf(i) >= 0) continue; if (!Object.prototype.hasOwnProperty.call(obj, i)) continue; target[i] = obj[i]; } return target; }

  var _React = _interopRequireDefault(_react);

  var _classNames = _interopRequireDefault(_classnames);

  var _BootstrapMixin2 = _interopRequireDefault(_BootstrapMixin);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var NavItem = _React['default'].createClass({
    displayName: 'NavItem',

    mixins: [_BootstrapMixin2['default']],

    propTypes: {
      linkId: _React['default'].PropTypes.string,
      onSelect: _React['default'].PropTypes.func,
      active: _React['default'].PropTypes.bool,
      disabled: _React['default'].PropTypes.bool,
      href: _React['default'].PropTypes.string,
      role: _React['default'].PropTypes.string,
      title: _React['default'].PropTypes.node,
      eventKey: _React['default'].PropTypes.any,
      target: _React['default'].PropTypes.string,
      'aria-controls': _React['default'].PropTypes.string
    },

    getDefaultProps: function getDefaultProps() {
      return {
        href: '#'
      };
    },

    render: function render() {
      var _props = this.props;
      var role = _props.role;
      var linkId = _props.linkId;
      var disabled = _props.disabled;
      var active = _props.active;
      var href = _props.href;
      var title = _props.title;
      var target = _props.target;
      var children = _props.children;
      var ariaControls = _props['aria-controls'];

      var props = _objectWithoutProperties(_props, ['role', 'linkId', 'disabled', 'active', 'href', 'title', 'target', 'children', 'aria-controls']);

      var classes = {
        active: active,
        disabled: disabled
      };
      var linkProps = {
        role: role,
        href: href,
        title: title,
        target: target,
        id: linkId,
        onClick: this.handleClick,
        ref: 'anchor'
      };

      if (!role && href === '#') {
        linkProps.role = 'button';
      }

      return _React['default'].createElement(
        'li',
        _extends({}, props, { role: 'presentation', className: (0, _classNames['default'])(props.className, classes) }),
        _React['default'].createElement(
          'a',
          _extends({}, linkProps, { 'aria-selected': active, 'aria-controls': ariaControls }),
          children
        )
      );
    },

    handleClick: function handleClick(e) {
      if (this.props.onSelect) {
        e.preventDefault();

        if (!this.props.disabled) {
          this.props.onSelect(this.props.eventKey, this.props.href, this.props.target);
        }
      }
    }
  });

  module.exports = NavItem;
});
// eslint-disable-line react/prop-types