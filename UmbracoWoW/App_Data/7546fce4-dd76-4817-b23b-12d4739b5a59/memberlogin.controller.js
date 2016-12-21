angular.module('umbraco').controller('MemberLogin.MemberLoginController', [

    '$scope',
    '$http',
    'editorState',
    'contentResource',

    function ($scope, $http, editorState, contentResource) {

        // Check if you are creating a new member
        $scope.isNew = editorState.current.id <= 0;

        // Define the login as member function
        $scope.loginAsMember = function () {

            // ### Setup cookie
            var url = 'backoffice/memberlogin/memberlogin/dologin';

            // Get the current member id using the editorState
            var _memberId = editorState.current.id;

            // Do Login
            $http.post(
                url,
                _memberId).then(
                function (response) {

                    // ### Redirect
                    // Get the redirect page from config
                    var urlPageRedirect = $scope.model.config.memberRedirectPage;

                    // Check if page is set in the config
                    if (urlPageRedirect)
                    {
                        contentResource.getNiceUrl(urlPageRedirect).then(function (data) {
                            window.open(data, '_blank') // Get the first url
                        });
                    } else {
                        // Open the root page
                        window.open('/', '_blank');
                    }
                },
                function (error) { }
            );
        };
    }
]);