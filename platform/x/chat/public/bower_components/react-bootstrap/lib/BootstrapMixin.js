define(['exports', 'module', './styleMaps', './utils/CustomPropTypes'], function (exports, module, _styleMaps, _utilsCustomPropTypes) {
  'use strict';

  function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

  var _styleMaps2 = _interopRequireDefault(_styleMaps);

  var _CustomPropTypes = _interopRequireDefault(_utilsCustomPropTypes);

  console.warn('This file is deprecated, and will be removed in v0.24.0. Use react-bootstrap.js or react-bootstrap.min.js instead.');
  console.warn('You can read more about it at https://github.com/react-bootstrap/react-bootstrap/issues/693');

  var BootstrapMixin = {
    propTypes: {
      /**
       * bootstrap className
       * @private
       */
      bsClass: _CustomPropTypes['default'].keyOf(_styleMaps2['default'].CLASSES),
      /**
       * Style variants
       * @type {("default"|"primary"|"success"|"info"|"warning"|"danger"|"link")}
       */
      bsStyle: _CustomPropTypes['default'].keyOf(_styleMaps2['default'].STYLES),
      /**
       * Size variants
       * @type {("xsmall"|"small"|"medium"|"large")}
       */
      bsSize: _CustomPropTypes['default'].keyOf(_styleMaps2['default'].SIZES)
    },

    getBsClassSet: function getBsClassSet() {
      var classes = {};

      var bsClass = this.props.bsClass && _styleMaps2['default'].CLASSES[this.props.bsClass];
      if (bsClass) {
        classes[bsClass] = true;

        var prefix = bsClass + '-';

        var bsSize = this.props.bsSize && _styleMaps2['default'].SIZES[this.props.bsSize];
        if (bsSize) {
          classes[prefix + bsSize] = true;
        }

        var bsStyle = this.props.bsStyle && _styleMaps2['default'].STYLES[this.props.bsStyle];
        if (this.props.bsStyle) {
          classes[prefix + bsStyle] = true;
        }
      }

      return classes;
    },

    prefixClass: function prefixClass(subClass) {
      return _styleMaps2['default'].CLASSES[this.props.bsClass] + '-' + subClass;
    }
  };

  module.exports = BootstrapMixin;
});