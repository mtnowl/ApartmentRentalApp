export default ({ store, route, redirect, error }) => {
  // Check if user is connected first
  if (!store.state.auth.loggedIn) return redirect('/login');

  // Get authorizations for matched routes (with children routes too)
  const authorizationLevels = route.meta.map((meta) => {
    if (meta.auth && typeof meta.auth.authority !== 'undefined')
      return meta.auth.authority;
    return 0;
  });
  // Get highest authorization level
  const highestAuthority = Math.max.apply(null, authorizationLevels);

  let authority;
  switch (store.state.auth.user.role) {
    case 'Admin':
      authority = 2;
      break;
    case 'Realtor':
      authority = 1;
      break;
    case 'Client':
      authority = 0;
      break;
    default:
      authority = -1;
      break;
  }
  if (authority < highestAuthority) {
    return error({
      statusCode: 401,
      message: 'You do not have permission to access this page'
    });
  }
};
