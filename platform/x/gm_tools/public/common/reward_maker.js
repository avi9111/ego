// Generated by CoffeeScript 1.12.7
(function() {
  var InputNumber, React, RewardMaker, Table, antd, boot;

  antd = require('antd');

  boot = require('react-bootstrap');

  React = require('react');

  InputNumber = antd.InputNumber;

  Table = boot.Table;

  RewardMaker = React.createClass({displayName: "RewardMaker",
    getInitialState: function() {
      return {
        rewards: [],
        curr_id: null,
        curr_num: 1
      };
    },
    onInputNumberChange: function(num) {
      return this.setState({
        curr_num: num
      });
    },
    onInputItemIDChange: function(event) {
      return this.setState({
        curr_id: event.target.value
      });
    },
    addReward: function() {
      var count, id, r;
      id = this.state.curr_id;
      count = this.state.curr_num;
      r = this.state.rewards;
      r.push({
        Id: id,
        Count: count
      });
      this.setState({
        rewards: r
      });
      if (this.props.onChange != null) {
        return this.props.onChange(r);
      }
    },
    cannotAddReward: function() {},
    getAddButton: function() {
      if ((this.state.curr_id != null) && this.state.curr_id !== "" && this.state.rewards.length < 5) {
        return React.createElement("button", {
          "className": "ant-btn ant-btn-primary",
          "onClick": this.addReward
        }, "增加");
      } else {
        return React.createElement("button", {
          "className": "ant-btn disable",
          "onClick": this.cannotAddReward
        }, "增加");
      }
    },
    del: function(k) {
      return (function(_this) {
        return function() {
          var ki, nr, ref, v;
          nr = [];
          ref = _this.state.rewards;
          for (ki in ref) {
            v = ref[ki];
            if (ki !== k) {
              nr.push(v);
            }
          }
          _this.setState({
            rewards: nr
          });
          if (_this.props.onChange != null) {
            return _this.props.onChange(nr);
          }
        };
      })(this);
    },
    getAllInfo: function(data) {
      var k, re, v;
      if (data == null) {
        return React.createElement("div", null, "UnKnown Info");
      }
      re = [];
      for (k in data) {
        v = data[k];
        re.push(React.createElement("tr", {
          "key": k
        }, React.createElement("td", null, v.Id), React.createElement("td", null, v.Count), React.createElement("td", {
          "className": 'row-flex row-flex-middle row-flex-start'
        }, React.createElement(boot.Button, {
          "bsStyle": 'danger',
          "disabled": false,
          "onClick": this.del(k)
        }, "删除"))));
      }
      return re;
    },
    getRewardList: function() {
      return React.createElement(Table, {
        "striped": true,
        "bordered": true,
        "condensed": true,
        "hover": true
      }, React.createElement("thead", null, React.createElement("tr", null, React.createElement("th", null, "ItemID"), React.createElement("th", null, "数量"), React.createElement("th", null, "操作"))), React.createElement("tbody", null, this.getAllInfo(this.state.rewards)));
    },
    render: function() {
      return React.createElement("div", null, React.createElement("div", {
        "className": "row-flex row-flex-middle row-flex-start"
      }, React.createElement("div", {
        "className": "col-2"
      }, "类型ID:"), React.createElement("input", {
        "className": "ant-input col-12",
        "type": "text",
        "id": "ItemID",
        "placeholder": "请输入类型ID",
        "onChange": this.onInputItemIDChange
      }), React.createElement("div", {
        "className": "col-2"
      }), React.createElement("div", {
        "className": "col-2"
      }, "数量:"), React.createElement(InputNumber, {
        "min": 1,
        "max": 1000000,
        "defaultValue": 1,
        "onChange": this.onInputNumberChange,
        "style": {
          width: 70
        }
      }), this.getAddButton()), React.createElement("hr", null), this.getRewardList());
    }
  });

  module.exports = RewardMaker;

}).call(this);
