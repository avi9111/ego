define(['exports', 'module', 'react', './utils/domUtils', './utils/EventListener'], function (exports, module, _react, _utilsDomUtils, _utilsEventListener) {
  'use strict';

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  var _React = _interopRequireDefault(_react);

  var _domUtils = _interopRequireDefault(_utilsDomUtils);

  var _EventListener = _interopRequireDefault(_utilsEventListener);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  /**
   * Checks whether a node is within
   * a root nodes tree
   *
   * @param {DOMElement} node
   * @param {DOMElement} root
   * @returns {boolean}
   */
  function isNodeInRoot(node, root) {
    while (node) {
      if (node === root) {
        return true;
      }
      node = node.parentNode;
    }

    return false;
  }

  var DropdownStateMixin = {
    getInitialState: function getInitialState() {
      return {
        open: false
      };
    },

    setDropdownState: function setDropdownState(newState, onStateChangeComplete) {
      if (newState) {
        this.bindRootCloseHandlers();
      } else {
        this.unbindRootCloseHandlers();
      }

      this.setState({
        open: newState
      }, onStateChangeComplete);
    },

    handleDocumentKeyUp: function handleDocumentKeyUp(e) {
      if (e.keyCode === 27) {
        this.setDropdownState(false);
      }
    },

    handleDocumentClick: function handleDocumentClick(e) {
      // If the click originated from within this component
      // don't do anything.
      // e.srcElement is required for IE8 as e.target is undefined
      var target = e.target || e.srcElement;
      if (isNodeInRoot(target, _React['default'].findDOMNode(this))) {
        return;
      }

      this.setDropdownState(false);
    },

    bindRootCloseHandlers: function bindRootCloseHandlers() {
      var doc = _domUtils['default'].ownerDocument(this);

      this._onDocumentClickListener = _EventListener['default'].listen(doc, 'click', this.handleDocumentClick);
      this._onDocumentKeyupListener = _EventListener['default'].listen(doc, 'keyup', this.handleDocumentKeyUp);
    },

    unbindRootCloseHandlers: function unbindRootCloseHandlers() {
      if (this._onDocumentClickListener) {
        this._onDocumentClickListener.remove();
      }

      if (this._onDocumentKeyupListener) {
        this._onDocumentKeyupListener.remove();
      }
    },

    componentWillUnmount: function componentWillUnmount() {
      this.unbindRootCloseHandlers();
    }
  };

  module.exports = DropdownStateMixin;
});